using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SampleProject.Infrastructure.Http.Extensions;
using SampleProject.Infrastructure.Http.HttpContentParsers.Interfaces;
using SampleProject.Infrastructure.Http.Models;

namespace SampleProject.Infrastructure.Http.HttpContentParsers;

public class MultiPartFormDataContentConverter : IHttpContentConverter
{
    public MediaTypeHeaderValue MediaType => new(ContentType.MultipartFormData.ToStringRepresentation());

    public HttpContent ConvertToHttpContent(object value)
    {
        if (value is not MultipartFormDataContent formDataContent)
        {
            throw new ArgumentException("Content is not MultipartFormDataContent", nameof(value));
        }

        return formDataContent;
    }

    public Task<TOutput> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        throw new NotSupportedException("Conversion from MultiPartFormDataContent is not supported");
    }
}
