using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SampleProject.Infrastructure.Http.Interfaces;

namespace SampleProject.Infrastructure.Http.Services;

internal class HttpConnectionService : IHttpConnectionService
{
    private readonly IHttpClientFactory _clientFactory;

    public HttpConnectionService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public HttpClient CreateHttpClient(string? clientName = null, TimeSpan? timeOut = null)
    {
        var client = string.IsNullOrWhiteSpace(clientName)
            ? _clientFactory.CreateClient()
            : _clientFactory.CreateClient(clientName);

        if (timeOut != null)
            client.Timeout = timeOut.Value;

        return client;
    }
    
    public async Task<HttpResponseMessage> SendRequestAsync(
        HttpClient httpClient,
        HttpRequestMessage httpRequestMessage,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default)
    {
        return await httpClient.SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken);
    }
}