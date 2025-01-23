using System.Reflection;
using System.Web;

using Microsoft.AspNetCore.WebUtilities;

namespace TodoApp.Web.Common.Http;

public class HttpQueryStringBuilder
{
    private readonly Dictionary<string, string?> queryParameters = new();

    public HttpQueryStringBuilder AddQuery<T>(T? obj) where T : class
    {
        if (obj is null) return this;

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach ( var property in properties)
        {
            if(property.GetValue(obj) is not null)
            {
               AddQuery(property.Name, property.GetValue(obj));
            }
        }

        return this;
    }

    public HttpQueryStringBuilder AddQuery(string name, object? value)
    {
        queryParameters[name.ToLower()] = HttpUtility.UrlEncode(value?.ToString());

        return this;
    }

    public string Build()
    {
        return QueryHelpers.AddQueryString(string.Empty, queryParameters!);
    }
}
