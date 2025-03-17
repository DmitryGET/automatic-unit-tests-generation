using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SampleProject.Infrastructure.Http.Extensions;
using SampleProject.Infrastructure.Http.HttpContentParsers.Interfaces;
using SampleProject.Infrastructure.Http.Models;

namespace SampleProject.Infrastructure.Http.HttpContentParsers;

public class XmlContentConverter : IHttpContentConverter
{
    public MediaTypeHeaderValue MediaType => new(ContentType.ApplicationXml.ToStringRepresentation());

    public HttpContent ConvertToHttpContent(object value)
    {
        var stringBuilder = new StringBuilder();
        var xmlSerializer = new XmlSerializer(value.GetType());

        using var writer = new StringWriter(stringBuilder);
        xmlSerializer.Serialize(writer, value);

        return new StringContent(stringBuilder.ToString(), Encoding.UTF8, MediaType);
    }

    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        var stringContent = await httpContent.ReadAsStringAsync();
        var xmlSerializer = new XmlSerializer(typeof(TOutput));

        using var reader = new StringReader(stringContent);
        return (TOutput)xmlSerializer.Deserialize(reader)!;
    }
}
