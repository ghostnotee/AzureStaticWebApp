using System.Text.Json;
using Microsoft.Azure.Functions.Worker.Http;

namespace Api.Extensions;

public static class HttpRequestExtensions
{
    public static string GetValueFromQuery(this HttpRequestData request, string key)
    {
        return request.Query.Get(key)!;
    }

    public static async Task<TEntity> GetValueFromBody<TEntity>(this HttpRequestData request)
    {
        var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
        return JsonSerializer.Deserialize<TEntity>(requestBody, JsonSerializerOptionsProvider.DefaultOptions)!;
    }
}