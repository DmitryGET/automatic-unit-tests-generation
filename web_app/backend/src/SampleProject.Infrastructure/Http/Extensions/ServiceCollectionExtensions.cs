using Microsoft.Extensions.DependencyInjection;
using SampleProject.Infrastructure.Http.Interfaces;
using SampleProject.Infrastructure.Http.Services;

namespace SampleProject.Infrastructure.Http.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHttpLogic(this IServiceCollection collection)
    {
        collection.AddHttpClient();

        collection.AddTransient<IHttpConnectionService, HttpConnectionService>();
        collection.AddTransient<IHttpRequestService, HttpRequestService>();

        return collection;
    }
}