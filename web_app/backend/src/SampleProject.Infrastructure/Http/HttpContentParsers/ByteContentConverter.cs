using System;
using System.Net.Http;
using System.Threading.Tasks;
using SampleProject.Infrastructure.Http.HttpContentParsers.Interfaces;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace SampleProject.Infrastructure.Http.HttpContentParsers;

public class ByteContentConverter : IHttpContentConverter
{
    public MediaTypeHeaderValue MediaType => new(string.Empty);

    public HttpContent ConvertToHttpContent(object value)
    {
        if (value.GetType() == typeof(byte[]))
        {
            return new ByteArrayContent((byte[])value);
        }

        throw new Exception($"Bad value for {nameof(ByteArrayContent)}");
    }

    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        var byteArray = await httpContent.ReadAsByteArrayAsync();
        return (TOutput)(object)byteArray;
    }
}