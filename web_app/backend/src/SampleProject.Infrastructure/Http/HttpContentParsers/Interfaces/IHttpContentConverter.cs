using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure.Http.HttpContentParsers.Interfaces;

public interface IHttpContentConverter
{
    MediaTypeHeaderValue MediaType { get; }
    
    HttpContent ConvertToHttpContent(object value);
    
    Task<TOutput> ConvertFromHttpContent<TOutput>(HttpContent httpContent);
}