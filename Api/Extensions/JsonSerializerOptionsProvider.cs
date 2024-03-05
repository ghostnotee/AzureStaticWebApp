using System.Text.Json;

namespace Api.Extensions;

public static class JsonSerializerOptionsProvider
{
    public static JsonSerializerOptions DefaultOptions => new(JsonSerializerDefaults.Web);
}