using System.Net;
using System.Net.Http.Headers;

namespace SampleProject.Infrastructure.Http.Models;

public record BaseHttpResponse
{
    public HttpStatusCode StatusCode { get; init; }

    public HttpResponseHeaders Headers { get; init; } = null!;
    
    public HttpContentHeaders ContentHeaders { get; init; } = null!;

    public bool IsSuccessStatusCode => (int)StatusCode is >= 200 and <= 299;
}
