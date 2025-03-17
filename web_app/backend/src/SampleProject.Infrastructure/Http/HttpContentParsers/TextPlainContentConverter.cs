using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SampleProject.Infrastructure.Http.Extensions;
using SampleProject.Infrastructure.Http.HttpContentParsers.Interfaces;
using SampleProject.Infrastructure.Http.Models;

namespace SampleProject.Infrastructure.Http.HttpContentParsers;

public class TextPlainContentConverter : IHttpContentConverter
{
    public MediaTypeHeaderValue MediaType => new(ContentType.TextPlain.ToStringRepresentation());

    public HttpContent ConvertToHttpContent(object value)
    {
        return new StringContent(value.ToString()!, Encoding.UTF8, MediaType);
    }

    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        return (TOutput)(object)await httpContent.ReadAsStringAsync();
    }
}
