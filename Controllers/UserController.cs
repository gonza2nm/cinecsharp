using System.Runtime.CompilerServices;
using AutoMapper;
using backend_cine.Dbcontext;
using backend_cine.DTOs;
using backend_cine.Interfaces;
using backend_cine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_cine.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(DbContextCinema dbContext, IMapper mapper) : ControllerBase, IRepository<UserDTO, UserRequestDTO>
{
  private readonly IMapper _mapper = mapper;
  private readonly DbContextCinema _dbContext = dbContext;

  [HttpGet]
  public async Task<ActionResult<ResponseList<UserDTO>>> FindAll()
  {
    var res = new ResponseList<UserDTO> { Status = "", Message = "", Data = [], Error = null };
    try
    {
      var usersDB = await _dbContext.Users.ToListAsync();
      var usersDTO = _mapper.Map<List<UserDTO>>(usersDB);
      res.UpdateValues("200", "Users found succesfully", usersDTO, null);
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
  [HttpGet("{id}")]
  public async Task<ActionResult<ResponseOne<UserDTO>>> FindOne(long id)
  {
    var res = new ResponseOne<UserDTO> { Status = "", Message = "", Data = null, Error = null };
    if (id <= 0)
    {
      res.UpdateValues("400", "Invalid User ID", null, "Bad Request");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    try
    {
      var userDB = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
      if (userDB is null)
      {
        res.UpdateValues("404", $"User with id: {id} not found", null, "Not Found");
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      var userDTO = _mapper.Map<UserDTO>(userDB);
      res.UpdateValues("200", "User found succesfully", userDTO, null);
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

  [HttpPost]
  public async Task<ActionResult<ResponseOne<UserDTO>>> Add(UserRequestDTO userBody)
  {
    var res = new ResponseOne<UserDTO> { Status = "", Message = "", Data = null, Error = null };
    if (!ModelState.IsValid || userBody.Dni == String.Empty || userBody.Email == String.Empty || userBody.Password == String.Empty)
    {
      res.UpdateValues("400", "Incorrect or Invalid data", null, null);
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    using var transaction = await _dbContext.Database.BeginTransactionAsync();
    try
    {
      var userExists = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userBody.Email || u.Dni == userBody.Dni);
      if (userExists is not null)
      {
        if (userExists.Dni == userBody.Dni)
        {
          res.UpdateValues("409", "There is already a user with that DNI", null, "Conflict");
        }
        else
        {
          res.UpdateValues("409", "There is already a user with that EMAIL", null, "Conflict");
        }
        return StatusCode(StatusCodes.Status409Conflict, res);
      }
      //limito rol de usuario
      var userDB = _mapper.Map<User>(userBody);
      userDB.CinemaId = null;
      userDB.IsManager = false;
      await _dbContext.Users.AddAsync(userDB);
      await _dbContext.SaveChangesAsync();
      await transaction.CommitAsync();
      var user = _mapper.Map<UserDTO>(userDB);
      res.UpdateValues("201", "User created succesfully", user, null);
      return StatusCode(StatusCodes.Status400BadRequest, res);
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
  public async Task<ActionResult<ResponseOne<UserDTO>>> Update(long id, UserRequestDTO userBody)
  {
    var res = new ResponseOne<MovieRequestDTO> { Status = "", Message = "", Data = null, Error = null };
    if (!ModelState.IsValid || id <= 0 || userBody.Dni == String.Empty || userBody.Email == String.Empty || userBody.Password == String.Empty)
    {
      res.UpdateValues("400", "Incorrect or empty data", null, "Bad Request");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    if (id != userBody.Id)
    {
      res.UpdateValues("400", "The id of the URI is diferent from the id in json object", null, "Bad Request");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    using var transaction = await _dbContext.Database.BeginTransactionAsync();
    try
    {
      var userEmail = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userBody.Email);
      var userDNI = await _dbContext.Users.FirstOrDefaultAsync(u => u.Dni == userBody.Dni);
      if (userEmail is not null || userDNI is not null)
      {
        if (userEmail is not null && userEmail.Id != userBody.Id)
        {
          res.UpdateValues("409", "The email already exist in other user", null, "Conflict");
          return StatusCode(StatusCodes.Status409Conflict, res);
        }
        if (userDNI is not null && userDNI.Id != userBody.Id)
        {
          res.UpdateValues("409", "The dni already exist in other user", null, "Conflict");
          return StatusCode(StatusCodes.Status409Conflict, res);
        }
      }
      var userDB = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
      if (userDB is null)
      {
        res.UpdateValues("404", $"User with id: {id} not found", null, "Not found");
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      _mapper.Map(userBody, userDB);
      userDB.Cinema = null;
      userDB.CinemaId = null;
      userDB.IsManager = false;
      _dbContext.Update(userDB);
      await _dbContext.SaveChangesAsync();
      await transaction.CommitAsync();
      var userDTO = _mapper.Map<UserDTO>(userDB);
      res.UpdateValues("200", $"User updated successfully", null, null);
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
  public async Task<ActionResult<ResponseOne<UserDTO>>> Delete(long id)
  {
    var res = new ResponseOne<MovieDTO> { Status = "", Message = "", Data = null, Error = null };
    if (id <= 0)
    {
      res.UpdateValues("400", "Invalid User ID", null, "Bad Request");
      return StatusCode(StatusCodes.Status400BadRequest, res);
    }
    using var transaction = await _dbContext.Database.BeginTransactionAsync();
    try
    {
      User? deleteUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
      if (deleteUser is null)
      {
        res.UpdateValues("404", $"User with id: {id} not found", null, "404 Not found");
        return StatusCode(StatusCodes.Status404NotFound, res);
      }
      _dbContext.Users.Remove(deleteUser);
      await _dbContext.SaveChangesAsync();
      await transaction.CommitAsync();
      res.UpdateValues("200", "User deleted successfully", null);
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
}