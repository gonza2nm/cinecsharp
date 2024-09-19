using Microsoft.AspNetCore.Mvc;
using backend_cine.Responses;
namespace backend_cine.Interfaces;

public interface ICrud<T>
{
	public Task<ActionResult<ResponseList<T>>> FindAll();
	public Task<ActionResult<ResponseOne<T>>> FindOne(long id);
	public Task<ActionResult<ResponseOne<T>>> Add(T obj);
	public Task<ActionResult<ResponseOne<T>>> Update(long id, T obj);
	public Task<ActionResult<ResponseOne<T>>> Delete(long id);
}