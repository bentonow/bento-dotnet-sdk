using System.Net;

namespace Bento;

public class BentoResponse<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public string? Error { get; init; }
    public HttpStatusCode StatusCode { get; init; }
}