using Microsoft.AspNetCore.Mvc;
using backend_cine.Models;
using backend_cine.Interfaces;
using backend_cine.Dbcontext;
using Microsoft.EntityFrameworkCore;
using backend_cine.DTOs;
using AutoMapper;

namespace backend_cine.Controllers;
[ApiController]
[Route("api/cinemas")]
public class CinemaController(DbContextCinema dbcontext, IMapper mapper) : ControllerBase, IRepository<CinemaDTO, CinemaRequestDTO>
{
	private readonly DbContextCinema _dbcontext = dbcontext;
	private readonly IMapper _mapper = mapper;

	//GET ALL
	[HttpGet]
	public async Task<ActionResult<ResponseList<CinemaDTO>>> FindAll()
	{
		var res = new ResponseList<CinemaDTO> { Status = "", Message = "", Data = [], Error = null };
		try
		{
			var cinemasDB = await _dbcontext.Cinemas.ToListAsync();
			var cinemas = _mapper.Map<List<CinemaDTO>>(cinemasDB);
			res.UpdateValues("200", "Found cinemas", cinemas);
			return StatusCode(StatusCodes.Status200OK, res);
		}
		catch (DbUpdateException dbEx)
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
	//GET ONE
	[HttpGet("{id}")]
	public async Task<ActionResult<ResponseOne<CinemaDTO>>> FindOne(long id)
	{
		var res = new ResponseOne<CinemaDTO> { Status = "", Message = "", Data = null, Error = null };
		if (id <= 0)
		{
			res.UpdateValues("400", "Invalid Cinema ID", null, "Bad Request");
			return StatusCode(StatusCodes.Status400BadRequest, res);
		}
		try
		{
			Cinema? cinemaDB = await _dbcontext.Cinemas.FirstOrDefaultAsync(c => c.Id == id);
			if (cinemaDB is null)
			{
				res.UpdateValues("404", $"Cinema with id: {id} not found", null, "Not Found");
				return StatusCode(StatusCodes.Status404NotFound, res);
			}
			var cinemaDTO = _mapper.Map<CinemaDTO>(cinemaDB);
			res.UpdateValues("200", "Found cinema", cinemaDTO);
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

	//ADD
	[HttpPost]
	public async Task<ActionResult<ResponseOne<CinemaDTO>>> Add([FromBody] CinemaRequestDTO cinema)
	{
		var res = new ResponseOne<CinemaDTO> { Status = "", Message = "", Data = null, Error = null };
		if (cinema is null || cinema.Name == null || cinema.Address == null)
		{
			res.UpdateValues("400", "Incorrect or empty data", null, "Invalid data");
			return StatusCode(StatusCodes.Status400BadRequest, res);
		}
		using var transaction = await _dbcontext.Database.BeginTransactionAsync();
		try
		{
			var cinemaToAdd = _mapper.Map<Cinema>(cinema);
			Cinema? existingCinema = await _dbcontext.Cinemas.FirstOrDefaultAsync(c => c.Address == cinema.Address);
			if (existingCinema != null)
			{
				res.UpdateValues("409", "There is already a cinema in that address", null, "Conflict in the database");
				return StatusCode(StatusCodes.Status409Conflict, res);
			}
			await _dbcontext.Cinemas.AddAsync(cinemaToAdd);
			await _dbcontext.SaveChangesAsync();
			await transaction.CommitAsync();
			var cinemaDTO = _mapper.Map<CinemaDTO>(cinemaToAdd);
			res.UpdateValues("201", "Cinema successfully created", cinemaDTO);
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
	public async Task<ActionResult<ResponseOne<CinemaDTO>>> Update(long id, CinemaRequestDTO cinemaBody)
	{
		var res = new ResponseOne<CinemaDTO> { Status = "", Message = "", Data = null, Error = null };
		if (id != cinemaBody.Id)
		{
			res.UpdateValues("400", "The id of the URI is diferent from the id in json object", null, "Bad Request");
			return StatusCode(StatusCodes.Status400BadRequest, res);
		}
		using var transaction = await _dbcontext.Database.BeginTransactionAsync();
		try
		{
			Cinema? updateCinema = await _dbcontext.Cinemas.FirstOrDefaultAsync(c => c.Id == id);
			if (updateCinema is null)
			{
				res.UpdateValues("404", $"Cinema with id: {id} not found", null, "Not found");
				return StatusCode(StatusCodes.Status404NotFound, res);
			}
			Cinema? addressCinema = await _dbcontext.Cinemas.FirstOrDefaultAsync(c => c.Address == cinemaBody.Address);
			if (addressCinema is not null && addressCinema.Id != updateCinema.Id)
			{
				res.UpdateValues("409", "The address already exist in another cinema", null, "Conflict in the data");
				return StatusCode(StatusCodes.Status409Conflict, res);
			}
			updateCinema.Name = cinemaBody.Name;
			updateCinema.Address = cinemaBody.Address;
			_dbcontext.Cinemas.Update(updateCinema);
			await _dbcontext.SaveChangesAsync();
			await transaction.CommitAsync();
			res.UpdateValues("200", "Cinema updated successfully", null);
			return StatusCode(StatusCodes.Status200OK, res);
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
	public async Task<ActionResult<ResponseOne<CinemaDTO>>> Delete(long id)
	{
		var res = new ResponseOne<CinemaDTO> { Status = "", Message = "", Data = null, Error = null };
		if (id <= 0)
		{
			res.UpdateValues("400", "Invalid Cinema ID", null, "Bad Request");
			return StatusCode(StatusCodes.Status400BadRequest, res);
		}
		try
		{
			Cinema? deleteCinema = await _dbcontext.Cinemas.FirstOrDefaultAsync(c => c.Id == id);
			if (deleteCinema is null)
			{
				res.UpdateValues("404", $"Cinema with id: {id} not found", null, "404 Not found");
				return StatusCode(StatusCodes.Status404NotFound, res);
			}
			_dbcontext.Cinemas.Remove(deleteCinema);
			await _dbcontext.SaveChangesAsync();
			res.UpdateValues("200", "Cinema deleted successfully", null);
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
