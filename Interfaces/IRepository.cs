using backend_cine.Models;
using Microsoft.AspNetCore.Mvc;
namespace backend_cine.Interfaces;

public interface IRepository<T>
{
	public Task<ActionResult<ResponseList<T>>> FindAll();
	public Task<ActionResult<ResponseOne<T>>> FindOne(long id);
	public Task<ActionResult<ResponseOne<T>>> Add(T obj);
	public Task<ActionResult<ResponseOne<T>>> Update(long id, T obj);
	public Task<ActionResult<ResponseOne<T>>> Delete(long id);
}