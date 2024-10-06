using AutoMapper;
using backend_cine.Dbcontext;
using backend_cine.DTOs;
using backend_cine.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_cine.Services;

public class TheaterService(DbContextCinema dbContext, IMapper mapper)
{
  private readonly DbContextCinema _dbContext = dbContext;
  private readonly IMapper _mapper = mapper;

  public async Task<List<Theater>> GetTheatersAsync(string? opt = null)
  {
    if (opt is not null)
    {
      if (opt.Equals("rows", StringComparison.OrdinalIgnoreCase))
      {
        return await _dbContext.Theaters.Include(t => t.Rows).ToListAsync();
      }
    }
    return await _dbContext.Theaters.ToListAsync();
  }


  public async Task<Theater?> GetTheaterByIdAsync(long id, string? opt = null)
  {
    if (opt is not null)
    {
      if (opt.Equals("rows&seats", StringComparison.OrdinalIgnoreCase))
      {
        return await _dbContext.Theaters.Include(t => t.Rows).ThenInclude(r => r.Seats).FirstOrDefaultAsync(t => t.Id == id);
      }
    }
    return await _dbContext.Theaters.FirstOrDefaultAsync(t => t.Id == id);
  }

  public async Task<Cinema?> SearchCinema(long id)
  {
    return await _dbContext.Cinemas.FirstOrDefaultAsync(c => c.Id == id);
  }

  public async Task<ResponseOne<TheaterDTO>> CreateTheaterAsync(TheaterRequestDTO theaterBody)
  {
    var res = new ResponseOne<TheaterDTO> { Status = "", Message = "", Data = null, Error = null };
    using var transaction = await _dbContext.Database.BeginTransactionAsync();
    try
    {
      var theaterToAdd = _mapper.Map<Theater>(theaterBody);
      var cinema = await SearchCinema(theaterBody.CinemaId);
      if (cinema == null)
      {
        res.UpdateValues("404", $"Cinema with {theaterBody.CinemaId} doesn't exist", null, "Not Found cinema while adding the theater");
        return res;
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
      await transaction.CommitAsync();

      var theaterDTO = _mapper.Map<TheaterDTO>(theaterToAdd);
      res.UpdateValues("201", "Theater successfully created", theaterDTO);
      return res;
    }
    catch (Exception ex)
    {
      await transaction.RollbackAsync();
      res.UpdateValues("500", "An error occurred while processing your request.", null, ex.Message);
      return res;
    }
  }

  public async Task<ResponseOne<TheaterDTO>> UpdateTheaterAsync(TheaterRequestDTO theaterBody)
  {
    using var transaction = await _dbContext.Database.BeginTransactionAsync();
    var res = new ResponseOne<TheaterDTO> { Status = "", Message = "", Data = null, Error = null };
    try
    {
      TheaterDTO theaterDTO;
      var theaterDB = await GetTheaterByIdAsync(theaterBody.Id, "rows&seats");
      if (theaterDB == null)
      {
        res.UpdateValues("404", $"Theater with id: {theaterBody.Id} not found", null, "Not Found");
        return res;
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
        return res;
      }
      if (seatsDiff == 0 && rowsDiff == 0)
      {
        //solo se cambia el nombre  y finaliza
        _dbContext.Update(theaterDB);
        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
        theaterDTO = _mapper.Map<TheaterDTO>(theaterDB);
        res.UpdateValues("200", "Theater Updated Succesfully", theaterDTO, null);
        return res;
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
        return res;
      }
    }
    catch (Exception ex)
    {
      await transaction.RollbackAsync();
      res.UpdateValues("500", "An error occurred while processing your request.", null, ex.Message);
      return res;
    }
  }
}