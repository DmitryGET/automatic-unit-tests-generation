using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SampleProject.Infrastructure.Http.Extensions;
using SampleProject.Infrastructure.Http.HttpContentParsers.Interfaces;
using SampleProject.Infrastructure.Http.Models;

namespace SampleProject.Infrastructure.Http.HttpContentParsers;

public class JsonContentConverter : IHttpContentConverter
{
    public MediaTypeHeaderValue MediaType => new(ContentType.ApplicationJson.ToStringRepresentation());

    public HttpContent ConvertToHttpContent(object value)
    {
        var jsonString = JsonSerializer.Serialize(value);
        return new StringContent(jsonString, Encoding.UTF8, MediaType);
    }

    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        return await httpContent.ReadFromJsonAsync<TOutput>();
    }
}
