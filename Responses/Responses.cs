namespace backend_cine.Responses;

public class ResponseList<T>
{
  public required string message { get; set; }
  public required List<T> data { get; set; }
  public required string? error { get; set; }
}

public class ResponseOne<T>
{
  public required string message { get; set; }
  public required T? data { get; set; }
  public required string? error { get; set; }
}