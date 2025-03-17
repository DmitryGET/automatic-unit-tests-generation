using System.Collections.Generic;
using SampleProject.Infrastructure.Http.Models;

namespace SampleProject.Infrastructure.Http.Extensions;

public static class ContentTypeExtensions
{
    private static readonly Dictionary<ContentType, string> ContentTypeStrings = new()
    {
        { ContentType.Unknown, "unknown" },
        { ContentType.ApplicationJson, "application/json" },
        { ContentType.XWwwFormUrlEncoded, "application/x-www-form-urlencoded" },
        { ContentType.Binary, "binary" },
        { ContentType.ApplicationXml, "application/xml" },
        { ContentType.MultipartFormData, "multipart/form-data" },
        { ContentType.TextXml, "text/xml" },
        { ContentType.TextPlain, "text/plain" },
        { ContentType.ApplicationJwt, "application/jwt" },
        { ContentType.ApplicationOctetStream, "application/octet-stream" }
    };
    
    public static string ToStringRepresentation(this ContentType contentType)
    {
        return ContentTypeStrings[contentType];
    }
}