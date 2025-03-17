using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure.Http.Interfaces;

public interface IHttpConnectionService
{
    HttpClient CreateHttpClient(string? clientName = null, TimeSpan? timeOut = null);
    
    Task<HttpResponseMessage> SendRequestAsync(
        HttpClient httpClient,
        HttpRequestMessage httpRequestMessage,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default);
}