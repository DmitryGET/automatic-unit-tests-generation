using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SampleProject.Infrastructure.Http.Extensions;
using SampleProject.Infrastructure.Http.HttpContentParsers.Interfaces;
using SampleProject.Infrastructure.Http.Models;

namespace SampleProject.Infrastructure.Http.HttpContentParsers;

public class OctetStreamContentConverter : IHttpContentConverter
{
    public MediaTypeHeaderValue MediaType => new(ContentType.ApplicationOctetStream.ToStringRepresentation());

    public HttpContent ConvertToHttpContent(object value)
    {
        if (value is not byte[] bytes)
        {
            throw new ArgumentException("Content is not byte[]", nameof(value));
        }

        var content = new ByteArrayContent(bytes);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        return content;
    }

    public async Task<TOutput> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        ArgumentNullException.ThrowIfNull(httpContent);

        if (typeof(TOutput) == typeof(byte[]))
        {
            return (TOutput)(object)await httpContent.ReadAsByteArrayAsync();
        }

        throw new NotSupportedException($"Conversion to type '{typeof(TOutput)}' is not supported");
    }
}
