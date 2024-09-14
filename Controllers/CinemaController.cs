using Microsoft.AspNetCore.Mvc;
using backend_cine.Models;
using backend_cine.Interfaces;
using System.Net.Mime;
using backend_cine.Dbcontext;
using Microsoft.EntityFrameworkCore;
using backend_cine.DTOs;
using backend_cine.Responses;
using System.Net.Sockets;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
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
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<List<CinemaDTO>>> FindAll()
	{
		var cinemasDTO = new List<CinemaDTO>();
		var cinemasDB = await _context.Cinemas.ToListAsync();
		foreach (var item in cinemasDB)
		{
			cinemasDTO.Add(new CinemaDTO { Id = item.Id, Name = item.Name, Address = item.Address });
		}
		return Ok(cinemasDTO);
	}

	//GET ONE
	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<ResponseOne<CinemaDTO>>> FindOne(long id)
	{
		CinemaDTO cinemaDTO = new CinemaDTO { };
		Cinema? cinemaDB = await _context.Cinemas.FirstOrDefaultAsync(c => c.Id == id);
		if (cinemaDB is null)
		{
			return NotFound(new ResponseOne<CinemaDTO> { message = $"Cinema with id: {id} not found", error = "Not Found", data = null });
		}
		cinemaDTO.Id = cinemaDB.Id;
		cinemaDTO.Name = cinemaDB.Name;
		cinemaDTO.Address = cinemaDB.Address;
		return Ok(new ResponseOne<CinemaDTO> { message = "Found Cinema", data = cinemaDTO, error = null });
	}

	//ADD
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<ResponseOne<CinemaDTO>>> Add([FromBody] CinemaDTO cinemaDTO)
	{
		if (cinemaDTO is null)
		{
			return BadRequest(new ResponseOne<CinemaDTO>
			{
				message = "Cinema data is null",
				data = null,
				error = "Invalid data"
			});
		}
		var cinema = new Cinema
		{
			Name = cinemaDTO.Name,
			Address = cinemaDTO.Address
		};
		await _context.Cinemas.AddAsync(cinema);
		await _context.SaveChangesAsync();
		var cinema2 = new CinemaDTO
		{
			Id = cinema.Id,
			Name = cinema.Name,
			Address = cinema.Address
		};

		var responseCreated = new ResponseOne<CinemaDTO>
		{
			message = "Cinema successfully created",
			data = cinema2,
			error = null
		};

		return Created("api/cinemas", responseCreated);
	}

	[HttpPut("{id}")]
	public IActionResult Update(long id, CinemaDTO obj)
	{
		if (id != obj.Id)
		{
			return BadRequest();
		}
		var cinemas = new List<CinemaDTO>{
			new CinemaDTO{Id = 1, Name = "Showcase", Address = "Cordoba 2000, Rosario, Santa Fe"},
			new CinemaDTO{Id = 2, Name = "Cinepolis", Address = "Cordoba 2100, Granadero Baigorria, Santa Fe"}
		};
		var cinema = cinemas.FirstOrDefault(cinema => cinema.Id == obj.Id);
		if (cinema is null)
		{
			return NotFound();
		}
		//make a function to update cinema

		//retorna vacio, aunque deberia retornar que se hicieron correctamente los cambios
		Console.WriteLine("Se encontro el cine y se actualizo el cine: ", cinema.Name);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(long id)
	{
		var cinemas = new List<Cinema>{
			new Cinema{Id = 1, Name = "Showcase", Address = "Cordoba 2000, Rosario, Santa Fe"},
			new Cinema{Id = 2, Name = "Cinepolis", Address = "Cordoba 2100, Granadero Baigorria, Santa Fe"}
		};
		var cinema = cinemas.FirstOrDefault(cinema => cinema.Id == id);
		if (cinema is null)
		{
			return NotFound();
		}
		//make a function to delete cinema

		//retorna vacio, aunque deberia retornar que se hicieron correctamente los cambios
		Console.WriteLine("Se encontro el cine y se elimino el cine: ", cinema.Name);
		return NoContent();
	}

}
