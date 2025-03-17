using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure.Http.Extensions;


public static class HttpRequestMessageExtensions
{
    public static async Task<HttpRequestMessage> ShallowCloneAsync(this HttpRequestMessage requestMessage)
    {
        StreamContent? httpContent = null;

        if (requestMessage.Content != null)
        {
            var contentBuffer = new MemoryStream();

            await requestMessage.Content.CopyToAsync(contentBuffer);
            contentBuffer.Position = 0;

            httpContent = new StreamContent(contentBuffer);

            foreach (var header in requestMessage.Content.Headers)
                httpContent.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        var messageClone = new HttpRequestMessage
        {
            RequestUri = requestMessage.RequestUri,
            Method = requestMessage.Method,
            Version = requestMessage.Version,
            Content = httpContent
        };
        
        foreach (var headerPair in requestMessage.Headers)
            messageClone.Headers.Add(headerPair.Key, headerPair.Value);

        foreach (var option in requestMessage.Options)
            messageClone.Options.TryAdd(option.Key, option.Value);

        return messageClone;
    }
}