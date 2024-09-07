using Microsoft.AspNetCore.Mvc;
namespace backend_cine.Interfaces;

public interface ICrud<T>
{
	public ActionResult<List<T>> FindAll();
	public ActionResult<T> FindOne(long id);
	public IActionResult Add(T obj);
	public IActionResult Update(long id, T obj);
	public IActionResult Delete(long id);
}