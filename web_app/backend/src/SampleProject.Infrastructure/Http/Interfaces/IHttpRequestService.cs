using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using SampleProject.Infrastructure.Http.Models;

namespace SampleProject.Infrastructure.Http.Interfaces;

public interface IHttpRequestService
{
    Task<HttpResponseData<TResponse>> SendRequestAsync<TResponse>(
        HttpRequestData requestData,
        HttpConnectionData connectionData = default,
        IAsyncPolicy<HttpResponseMessage>? resiliencePolicy = null,
        CancellationToken cancellationToken = default) where TResponse : class;
}