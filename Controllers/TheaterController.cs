using System.Data.Common;
using AutoMapper;
using backend_cine.Dbcontext;
using backend_cine.DTOs;
using backend_cine.Interfaces;
using backend_cine.Models;
using backend_cine.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_cine.Controllers;

[ApiController]
[Route("api/theaters")]
public class TheaterController(DbContextCinema dbcontext, IMapper mapper, TheaterService service) : ControllerBase, IRepository<TheaterDTO, TheaterRequestDTO>
{
  private readonly DbContextCinema _dbContext = dbcontext;
  private readonly IMapper _mapper = mapper;
  private readonly TheaterService _service = service;

  //Find All
  [HttpGet]
  public async Task<ActionResult<ResponseList<TheaterDTO>>> FindAll()
  {
    var res = new ResponseList<TheaterDTO>() { Status = "", Message = "", Data = [], Error = null };
    try
    {
      var theatersDB = await _service.GetTheatersAsync("rows");
      var theaters = _mapper.Map<List<TheaterDTO>>(theatersDB);
      res.UpdateValues("200", "Found Theaters", theaters, null);
      return StatusCode(StatusCodes.Status200OK, res);
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
    if (id <= 0)
    {
      res.UpdateValues("400", "Invalid Theater ID", null, "Bad Request");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    try
    {
      var theaterDB = await _service.GetTheaterByIdAsync(id, "rows&seats");
      if (theaterDB is null)
      {
        res.UpdateValues("404", $"Theater with id: {id} not found", null, "Not Found");
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      var theater = _mapper.Map<TheaterDTO>(theaterDB);
      res.UpdateValues("200", "Found Theater", theater, null);
      return StatusCode(StatusCodes.Status200OK, res);
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
    var result = await _service.CreateTheaterAsync(theaterBody);
    var statusCode = result.Status switch
    {
      "201" => StatusCodes.Status201Created,
      "404" => StatusCodes.Status404NotFound,
      "500" => StatusCodes.Status500InternalServerError,
      _ => StatusCodes.Status500InternalServerError
    };
    return StatusCode(statusCode, result);
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
    var result = await _service.UpdateTheaterAsync(theaterBody);
    var statusCode = result.Status switch
    {
      "200" => StatusCodes.Status200OK,
      "404" => StatusCodes.Status404NotFound,
      "500" => StatusCodes.Status500InternalServerError,
      _ => StatusCodes.Status500InternalServerError
    };
    return StatusCode(statusCode, result);
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
    using var transaction = await _dbContext.Database.BeginTransactionAsync();
    try
    {
      var deleteTheater = await _service.GetTheaterByIdAsync(id);
      if (deleteTheater is null)
      {
        res.UpdateValues("404", $"Theater with id: {id} not found", null, "404 Not found");
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      _dbContext.Remove(deleteTheater);
      await _dbContext.SaveChangesAsync();
      await transaction.CommitAsync();
      res.UpdateValues("200", "Theater deleted successfully", null);
      return StatusCode(StatusCodes.Status200OK, res);
    }
    catch (Exception ex)
    {
      await transaction.RollbackAsync();
      res.UpdateValues("500", "An error occurred while processing your request.", null, ex.Message);
      return StatusCode(StatusCodes.Status500InternalServerError, res);
    }
  }
}