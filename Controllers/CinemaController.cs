using Microsoft.AspNetCore.Mvc;
using backend_cine.Models;
using backend_cine.Interfaces;
using System.Net.Mime;
using backend_cine.Dbcontext;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/cinemas")]
public class CinemaController : ControllerBase, ICrud<Cinema>
{

	private readonly DbContextCinema _context;

	public CinemaController(DbContextCinema context)
	{
		_context = context;
	}

	[HttpGet]
	public ActionResult<List<Cinema>> FindAll()
	{
		return new List<Cinema>
		{
			new Cinema{Id = 1, Name = "Showcase", Address = "Cordoba 2000, Rosario, Santa Fe"},
			new Cinema{Id = 2, Name = "Cinepolis", Address = "Cordoba 2100, Granadero Baigorria, Santa Fe"}
		};
	}

	[HttpGet("{id}")]
	public ActionResult<Cinema> FindOne(long id)
	{
		List<Cinema> cinemas = new List<Cinema>
		{
			new Cinema{Id = 1, Name = "Showcase", Address = "Cordoba 2000, Rosario, Santa Fe"},
			new Cinema{Id = 2, Name = "Cinepolis", Address = "Cordoba 2100, Granadero Baigorria, Santa Fe"}
		};

		Cinema? cinema = cinemas.FirstOrDefault(cinema => cinema.Id == id);
		if (cinema is null)
		{
			return NotFound();
		}
		return cinema;

	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public IActionResult Add([FromBody] Cinema obj)
	{
		if (obj is null)
		{
			return BadRequest();
		}
		return CreatedAtAction(nameof(FindOne), new { id = obj.Id }, obj);
	}

	[HttpPut("{id}")]
	public IActionResult Update(long id, Cinema obj)
	{
		if (id != obj.Id)
		{
			return BadRequest();
		}
		var cinemas = new List<Cinema>{
			new Cinema{Id = 1, Name = "Showcase", Address = "Cordoba 2000, Rosario, Santa Fe"},
			new Cinema{Id = 2, Name = "Cinepolis", Address = "Cordoba 2100, Granadero Baigorria, Santa Fe"}
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
