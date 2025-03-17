using System;
using SampleProject.Infrastructure.Http.HttpContentParsers;
using SampleProject.Infrastructure.Http.HttpContentParsers.Interfaces;
using SampleProject.Infrastructure.Http.Models;

namespace SampleProject.Infrastructure.Http.Factories;

public static class HttpContentConverterFactory
{
    public static IHttpContentConverter CreateConverter(ContentType contentType)
    {
        return contentType switch
        {
            ContentType.ApplicationJson => new JsonContentConverter(),
            ContentType.ApplicationXml => new XmlContentConverter(),
            ContentType.TextXml => new XmlContentConverter(),
            ContentType.Binary => new ByteContentConverter(),
            ContentType.XWwwFormUrlEncoded => new XWwwFormUrlEncodedConverter(),
            ContentType.TextPlain => new TextPlainContentConverter(),
            ContentType.MultipartFormData => new MultiPartFormDataContentConverter(),
            ContentType.ApplicationOctetStream => new OctetStreamContentConverter(),
            ContentType.ApplicationJwt => throw new NotSupportedException(),
            ContentType.Unknown => throw new NotSupportedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(contentType), contentType, null)
        };
    }
}
