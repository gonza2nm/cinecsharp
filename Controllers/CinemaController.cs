using Microsoft.AspNetCore.Mvc;
using backend_cine.Models;
using backend_cine.Interfaces;
using backend_cine.Dbcontext;
using Microsoft.EntityFrameworkCore;
using backend_cine.DTOs;
using backend_cine.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend_cine.Controllers;
[ApiController]
[Produces("application/json")]
[Route("api/cinemas")]
public class CinemaController : ControllerBase, ICrud<CinemaDTO>
{

	private readonly DbContextCinema _context;

	public CinemaController(DbContextCinema context)
	{
		_context = context;
	}

	//GET ALL
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ResponseList<CinemaDTO>>> FindAll()
	{
		try
		{
			var cinemasDTO = new List<CinemaDTO>();
			var cinemasDB = await _context.Cinemas.ToListAsync();
			foreach (var item in cinemasDB)
			{
				cinemasDTO.Add(new CinemaDTO { Id = item.Id, Name = item.Name, Address = item.Address });
			}
			return StatusCode(StatusCodes.Status200OK, new ResponseList<CinemaDTO> { message = "Found cinemas", data = cinemasDTO, error = null });
		}
		catch (DbUpdateException dbEx)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new ResponseOne<CinemaDTO> { message = "Database error occurred.", error = dbEx.Message, data = null });
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new ResponseOne<CinemaDTO> { message = "An error occurred while processing your request.", error = ex.Message, data = null });
		}

	}

	//GET ONE
	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ResponseOne<CinemaDTO>>> FindOne(long id)
	{
		if (id <= 0)
		{
			return StatusCode(StatusCodes.Status400BadRequest, new ResponseOne<CinemaDTO> { message = "Invalid Cinema ID", error = "Bad Request", data = null });
		}
		try
		{
			Cinema? cinemaDB = await _context.Cinemas.FirstOrDefaultAsync(c => c.Id == id);
			if (cinemaDB is null)
			{
				return StatusCode(StatusCodes.Status404NotFound, new ResponseOne<CinemaDTO> { message = $"Cinema with id: {id} not found", error = "Not Found", data = null });
			}
			var cinemaDTO = new CinemaDTO
			{
				Id = cinemaDB.Id,
				Name = cinemaDB.Name,
				Address = cinemaDB.Address
			};
			return StatusCode(StatusCodes.Status200OK, new ResponseOne<CinemaDTO> { message = "Found Cinema", data = cinemaDTO, error = null });
		}
		catch (DbUpdateException dbEx)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new ResponseOne<CinemaDTO> { message = "Database error occurred.", error = dbEx.Message, data = null });
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new ResponseOne<CinemaDTO> { message = "An error occurred while processing your request.", error = ex.Message, data = null });
		}
	}

	//ADD
	[HttpPost]
	[Consumes("application/json")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ResponseOne<CinemaDTO>>> Add([FromBody] CinemaDTO cinema)
	{
		if (cinema is null || cinema.Name == null || cinema.Address == null)
		{
			return StatusCode(StatusCodes.Status400BadRequest, new ResponseOne<CinemaDTO> { message = "Cinema data is null", data = null, error = "Invalid data" });
		}
		try
		{
			var cinemaToAdd = new Cinema
			{
				Name = cinema.Name,
				Address = cinema.Address
			};
			Cinema? existingCinema = await _context.Cinemas.FirstOrDefaultAsync(c => c.Address == cinema.Address);
			if (existingCinema != null)
			{
				return StatusCode(StatusCodes.Status409Conflict, new ResponseOne<CinemaDTO> { message = "There is already a cinema in that address", data = null, error = "Conflict in the database" });
			}
			await _context.Cinemas.AddAsync(cinemaToAdd);
			await _context.SaveChangesAsync();
			var cinema2 = new CinemaDTO
			{
				Id = cinemaToAdd.Id,
				Name = cinemaToAdd.Name,
				Address = cinemaToAdd.Address
			};
			return StatusCode(StatusCodes.Status201Created, new ResponseOne<CinemaDTO> { message = "Cinema successfully created ", data = cinema2, error = null });
		}
		catch (DbUpdateException dbEx)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new ResponseOne<CinemaDTO> { message = "Database error occurred.", error = dbEx.Message, data = null });
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new ResponseOne<CinemaDTO> { message = "An error occurred while processing your request.", error = ex.Message, data = null });
		}
	}

	[HttpPut("{id}")]
	[Consumes("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ResponseOne<CinemaDTO>>> Update(long id, CinemaDTO cinemaBody)
	{
		if (id != cinemaBody.Id)
		{
			return StatusCode(StatusCodes.Status400BadRequest, new ResponseOne<CinemaDTO> { message = "The id of the URI is diferent from the id in json object", error = "Bad Request", data = null });
		}

		try
		{
			Cinema? updateCinema = await _context.Cinemas.FirstOrDefaultAsync(c => c.Id == id);
			if (updateCinema is null)
			{
				return StatusCode(StatusCodes.Status404NotFound, new ResponseOne<CinemaDTO> { message = $"Cinema with id: {id} not found", error = "Not found", data = null });
			}
			Cinema? addressCinema = await _context.Cinemas.FirstOrDefaultAsync(c => c.Address == cinemaBody.Address);
			if (addressCinema is not null && addressCinema.Id != updateCinema.Id)
			{
				return StatusCode(StatusCodes.Status409Conflict, new ResponseOne<CinemaDTO> { message = "The address already exist in another cinema", error = "Conflict in the data", data = null });
			}
			updateCinema.Name = cinemaBody.Name;
			updateCinema.Address = cinemaBody.Address;
			_context.Cinemas.Update(updateCinema);
			await _context.SaveChangesAsync();
			return StatusCode(StatusCodes.Status200OK, new ResponseOne<CinemaDTO> { message = "Cinema updated successfully", error = null, data = null });
		}
		catch (DbUpdateException dbEx)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new ResponseOne<CinemaDTO> { message = "Database error occurred.", error = dbEx.Message, data = null });
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new ResponseOne<CinemaDTO> { message = "An error occurred while processing your request.", error = ex.Message, data = null });

		}
	}

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ResponseOne<CinemaDTO>>> Delete(long id)
	{
		if (id <= 0)
		{
			return StatusCode(StatusCodes.Status400BadRequest, new ResponseOne<CinemaDTO> { message = "Invalid Cinema ID", error = "Bad Request", data = null });
		}
		try
		{
			Cinema? deleteCinema = await _context.Cinemas.FirstOrDefaultAsync(c => c.Id == id);
			if (deleteCinema is null)
			{
				return StatusCode(StatusCodes.Status404NotFound, new ResponseOne<CinemaDTO> { message = $"Cinema with id: {id} not found", error = "404 Not found", data = null });
			}
			_context.Cinemas.Remove(deleteCinema);
			await _context.SaveChangesAsync();
			return StatusCode(StatusCodes.Status200OK, new ResponseOne<CinemaDTO> { message = "Cinema deleted successfully", error = null, data = null });
		}
		catch (DbUpdateException dbEx)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new ResponseOne<CinemaDTO> { message = "Database error occurred.", error = dbEx.Message, data = null });
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, new ResponseOne<CinemaDTO> { message = "An error occurred while processing your request.", error = ex.Message, data = null });
		}

	}

}
