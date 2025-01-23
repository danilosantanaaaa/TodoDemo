using Newtonsoft.Json;

using TodoApp.Application.Common.Models;
using TodoApp.Web.Common.Json;

namespace TodoApp.Web.Common.Http;

public static class HttpClientExtension
{
    public static async Task<PaginatedList<T?>> GetPaginatedListFromJsonAsync<T>(this HttpClient client, string requestUri)
    {
        string jsonRead = await client.GetStringAsync(requestUri);
        var setting = new JsonSerializerSettings()
        {
            ContractResolver = new PrivateResolver(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        };
        return JsonConvert.DeserializeObject<PaginatedList<T?>>(jsonRead, setting)!;
    }
}
