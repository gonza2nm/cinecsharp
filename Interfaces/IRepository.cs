using backend_cine.Models;
using Microsoft.AspNetCore.Mvc;
namespace backend_cine.Interfaces;

public interface IRepository<T, TReq>
{
	public Task<ActionResult<ResponseList<T>>> FindAll();
	public Task<ActionResult<ResponseOne<T>>> FindOne(long id);
	public Task<ActionResult<ResponseOne<T>>> Add(TReq obj);
	public Task<ActionResult<ResponseOne<T>>> Update(long id, TReq obj);
	public Task<ActionResult<ResponseOne<T>>> Delete(long id);
}