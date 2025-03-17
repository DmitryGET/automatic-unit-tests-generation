using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using SampleProject.Infrastructure.Http.Extensions;
using SampleProject.Infrastructure.Http.HttpContentParsers.Interfaces;
using SampleProject.Infrastructure.Http.Models;

namespace SampleProject.Infrastructure.Http.HttpContentParsers;

public class XWwwFormUrlEncodedConverter : IHttpContentConverter
{
    public MediaTypeHeaderValue MediaType => new(ContentType.XWwwFormUrlEncoded.ToStringRepresentation());

    public HttpContent ConvertToHttpContent(object value)
    {
        if (value is IEnumerable<KeyValuePair<string, string>> list)
            return new FormUrlEncodedContent(list);

        throw new Exception($"Bad value for {nameof(FormUrlEncodedContent)}");
    }

    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        var contentString = await httpContent.ReadAsStringAsync();
        var formData = HttpUtility.ParseQueryString(contentString);

        var result = Activator.CreateInstance<TOutput>();
        var resultType = typeof(TOutput);

        foreach (var key in formData.AllKeys)
        {
            var propertyInfo = resultType.GetProperty(key);

            if (propertyInfo == null || !propertyInfo.CanWrite)
                continue;

            var value = formData[key];
            propertyInfo.SetValue(result, Convert.ChangeType(value, propertyInfo.PropertyType), null);
        }

        return result;
    }
}
