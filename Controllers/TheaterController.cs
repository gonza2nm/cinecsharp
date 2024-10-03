using System.Data.Common;
using AutoMapper;
using backend_cine.Dbcontext;
using backend_cine.DTOs;
using backend_cine.Interfaces;
using backend_cine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace backend_cine.Controllers;

[ApiController]
[Route("api/theaters")]
public class TheaterController(DbContextCinema dbcontext, IMapper mapper) : ControllerBase, IRepository<TheaterDTO, TheaterRequestDTO>
{
  private readonly DbContextCinema _dbContext = dbcontext;
  private readonly IMapper _mapper = mapper;

  //Find All
  [HttpGet]
  public async Task<ActionResult<ResponseList<TheaterDTO>>> FindAll()
  {
    var res = new ResponseList<TheaterDTO>() { Status = "", Message = "", Data = [], Error = null };
    try
    {
      var theatersDB = await _dbContext.Theaters.Include(t => t.Rows).ToListAsync();
      var theaters = _mapper.Map<List<TheaterDTO>>(theatersDB);
      res.UpdateValues("200", "Found Theaters", theaters, null);
      return StatusCode(StatusCodes.Status200OK, res);
    }
    catch (DbException dbEx)
    {
      res.UpdateValues("500", "Database error occurred.", [], dbEx.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
    catch (Exception ex)
    {
      res.UpdateValues("500", "An error occurred while processing your request.", [], ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
  }
  [HttpGet("{id}")]
  public async Task<ActionResult<ResponseOne<TheaterDTO>>> FindOne(long id)
  {
    var res = new ResponseOne<TheaterDTO>() { Status = "", Message = "", Data = null, Error = null };
    try
    {
      var theaterDB = await _dbContext.Theaters.Include(t => t.Rows).ThenInclude(r => r.Seats).FirstOrDefaultAsync(t => t.Id == id);
      if (theaterDB is null)
      {
        res.UpdateValues("404", $"Theater with id: {id} not found", null, "Not Found");
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      var theater = _mapper.Map<TheaterDTO>(theaterDB);
      res.UpdateValues("200", "Found Theater", theater, null);
      return StatusCode(StatusCodes.Status200OK, res);
    }
    catch (DbException dbEx)
    {
      res.UpdateValues("500", "Database error occurred.", null, dbEx.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
    catch (Exception ex)
    {
      res.UpdateValues("500", "An error occurred while processing your request.", null, ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
  }

  [HttpPost]
  public async Task<ActionResult<ResponseOne<TheaterDTO>>> Add(TheaterRequestDTO theaterBody)
  {
    var res = new ResponseOne<TheaterDTO> { Status = "", Message = "", Data = null, Error = null };
    if (theaterBody is null || !ModelState.IsValid || theaterBody.NumRows <= 0 || theaterBody.MaxSeats <= 0)
    {
      res.UpdateValues("400", "Incorrect or empty data", null, "Invalid data");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    else if (theaterBody.MaxSeats < theaterBody.NumRows)
    {
      res.UpdateValues("400", "The number of seats must be greater than the number of rows", null, "Invalid data");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    using var transaction = await _dbContext.Database.BeginTransactionAsync();
    try
    {
      var theaterToAdd = _mapper.Map<Theater>(theaterBody);
      var cinema = await _dbContext.Cinemas.FirstOrDefaultAsync(c => c.Id == theaterBody.CinemaId);
      if (cinema == null)
      {
        res.UpdateValues("404", $"Cinema with {theaterBody.CinemaId} doesn't exist", null, "Not Found cinema while adding the theater");
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      theaterToAdd.Cinema = cinema;
      await _dbContext.Theaters.AddAsync(theaterToAdd);
      await _dbContext.SaveChangesAsync();

      var seatForRow = theaterBody.MaxSeats / theaterBody.NumRows;
      var rest = theaterBody.MaxSeats % theaterBody.NumRows;
      var rows = new List<Row>();

      for (int i = 1; i <= theaterBody.NumRows; i++)
      {
        int seatsInThisRow = seatForRow + (i == 1 ? rest : 0);
        var row = new Row
        {
          RowNumber = i,
          Theater = theaterToAdd,
          TheaterId = theaterToAdd.Id,
          TotalCapacity = seatsInThisRow
        };
        rows.Add(row);
      }
      await _dbContext.Rows.AddRangeAsync(rows);
      await _dbContext.SaveChangesAsync();
      var seats = new List<Seat>();
      foreach (var row in rows)
      {
        for (int i = 1; i <= row.TotalCapacity; i++)
        {
          seats.Add(new Seat { Number = i, Row = row, RowId = row.Id });
        }
      }
      await _dbContext.Seats.AddRangeAsync(seats);
      await _dbContext.SaveChangesAsync();
      var theaterDTO = _mapper.Map<TheaterDTO>(theaterToAdd);
      await transaction.CommitAsync();
      res.UpdateValues("201", "Theater successfully created", theaterDTO);
      return StatusCode(StatusCodes.Status201Created, res);
    }
    catch (DbUpdateException dbEx)
    {
      await transaction.RollbackAsync();
      res.UpdateValues("500", "Database error occurred.", null, dbEx.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
    catch (Exception ex)
    {
      await transaction.RollbackAsync();
      res.UpdateValues("500", "An error occurred while processing your request.", null, ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<ResponseOne<TheaterDTO>>> Update(long id, TheaterRequestDTO theaterBody)
  {
    var res = new ResponseOne<TheaterDTO> { Status = "", Message = "", Data = null, Error = null };
    if (!ModelState.IsValid || id <= 0)
    {
      res.UpdateValues("400", "Incorrect or empty data", null, "Bad Request");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    if (id != theaterBody.Id)
    {
      res.UpdateValues("400", "The id of the URI is diferent from the id in json object", null, "Bad Request");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    using var transaction = await _dbContext.Database.BeginTransactionAsync();
    try
    {
      TheaterDTO theaterDTO;
      Theater? theaterDB = await _dbContext.Theaters.Include(t => t.Rows).ThenInclude(r => r.Seats).FirstOrDefaultAsync(t => t.Id == id);
      if (theaterDB == null)
      {
        res.UpdateValues("404", $"Theater with id: {id} not found", null, "Not Found");
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      int countCurrentSeats = 0;
      int currentRows = theaterDB.Rows.Count;
      int seatsDiff, rowsDiff;
      theaterDB.TheaterName = theaterBody.TheaterName;
      foreach (Row row in theaterDB.Rows)
      {
        countCurrentSeats += row.Seats.Count;
      }
      seatsDiff = countCurrentSeats - theaterBody.MaxSeats;
      rowsDiff = theaterDB.Rows.Count - theaterBody.NumRows;
      if (theaterBody.MaxSeats < theaterBody.NumRows)
      {
        res.UpdateValues("400", "The number of seats cannot be greater than the number of rows", null, "Bad Request");
        return StatusCode(StatusCodes.Status400BadRequest, res);
      }
      if (seatsDiff == 0 && rowsDiff == 0)
      {
        //solo se cambia el nombre  y finaliza
        _dbContext.Update(theaterDB);
        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
        theaterDTO = _mapper.Map<TheaterDTO>(theaterDB);
        res.UpdateValues("200", "Theater Updated Succesfully", theaterDTO, null);
        return StatusCode(StatusCodes.Status200OK, res);
      }
      else
      {
        var seatsForRow = theaterBody.MaxSeats / theaterBody.NumRows;
        var rest = theaterBody.MaxSeats % theaterBody.NumRows;
        if (rowsDiff < 0)
        {
          //crear nuevas filas con sus respectivas sillas
          for (int i = 0; i > rowsDiff; i--)
          {
            var row = new Row
            {
              RowNumber = currentRows + ((i - 1) * -1),
              Theater = theaterDB,
              TheaterId = theaterDB.Id,
              TotalCapacity = seatsForRow
            };
            await _dbContext.AddAsync(row);
            await _dbContext.SaveChangesAsync();
          }
        }
        else if (rowsDiff > 0)
        {
          //se achicaron la cantidad de filas, ajustar sillas por fila
          for (int i = 1; i <= rowsDiff; i++)
          {
            var rowToDelete = theaterDB.Rows.Last();
            _dbContext.Remove(rowToDelete);
            await _dbContext.SaveChangesAsync();
          }
        }
        //Modificar la cantidad de sillas
        _dbContext.Entry(theaterDB).Collection(t => t.Rows).Load();
        //funcionaba antes, revisar ahora
        var isFirstRow = true;
        foreach (var row in theaterDB.Rows)
        {
          int seatsInThisRow = 0;
          var totalSeatsAtTheMoment = row.Seats.Count;
          if (isFirstRow)
          {
            seatsInThisRow = seatsForRow + rest;
            isFirstRow = false;
          }
          else
          {
            seatsInThisRow = seatsForRow;
          }
          row.TotalCapacity = seatsInThisRow;
          _dbContext.Update(row);
          await _dbContext.SaveChangesAsync();
          if (totalSeatsAtTheMoment > seatsInThisRow)
          {
            //eliminar sillas
            int numSeatsToDelete = totalSeatsAtTheMoment - seatsInThisRow;
            for (int s = 0; s < numSeatsToDelete; s++)
            {
              var seatToRemove = row.Seats.Last();
              _dbContext.Remove(seatToRemove);
              await _dbContext.SaveChangesAsync();
            }
          }
          else if (totalSeatsAtTheMoment < seatsInThisRow)
          {
            //agregar sillas
            int numSeatsToAdd = seatsInThisRow - row.Seats.Count;
            for (int s = 1; s <= numSeatsToAdd; s++)
            {
              var seatToAdd = new Seat { Number = totalSeatsAtTheMoment + s, Row = row, RowId = row.Id };
              await _dbContext.AddAsync(seatToAdd);
              await _dbContext.SaveChangesAsync();
            }
          }
        }
        _dbContext.Entry(theaterDB).Collection(t => t.Rows).Load();
        await transaction.CommitAsync();
        theaterDTO = _mapper.Map<TheaterDTO>(theaterDB);
        res.UpdateValues("200", "Theater Updated Succesfully", theaterDTO, null);
        return StatusCode(StatusCodes.Status202Accepted, res);
      }
    }
    catch (DbUpdateException dbEx)
    {
      await transaction.RollbackAsync();
      res.UpdateValues("500", "Database error occurred.", null, dbEx.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
    catch (Exception ex)
    {
      await transaction.RollbackAsync();
      res.UpdateValues("500", "An error occurred while processing your request.", null, ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<ResponseOne<TheaterDTO>>> Delete(long id)
  {
    var res = new ResponseOne<TheaterDTO> { Status = "", Message = "", Data = null, Error = null };
    if (!ModelState.IsValid || id <= 0)
    {
      res.UpdateValues("400", "Invalid Theater ID", null, "Bad Request");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    try
    {
      var deleteTheater = await _dbContext.Theaters.FirstOrDefaultAsync(t => t.Id == id);
      if (deleteTheater is null)
      {
        res.UpdateValues("404", $"Theater with id: {id} not found", null, "404 Not found");
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      _dbContext.Remove(deleteTheater);
      await _dbContext.SaveChangesAsync();
      res.UpdateValues("200", "Theater deleted successfully", null);
      return StatusCode(StatusCodes.Status200OK, res);
    }
    catch (DbUpdateException dbEx)
    {
      res.UpdateValues("500", "Database error occurred.", null, dbEx.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
    catch (Exception ex)
    {
      res.UpdateValues("500", "An error occurred while processing your request.", null, ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
  }
}