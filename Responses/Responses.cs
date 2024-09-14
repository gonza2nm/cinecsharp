namespace backend_cine.Responses;

public class ResponseList<T>
{
  public string message { get; set; }
  public List<T> data { get; set; }
  public string? error { get; set; }
}

public class ResponseOne<T>
{
  public string message { get; set; }
  public T? data { get; set; }
  public string? error { get; set; }
}