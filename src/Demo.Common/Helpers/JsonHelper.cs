using System.Text.Json;
using System.Text.Json.Serialization;

namespace Demo.Common.Helpers;

public static class JsonHelper
{
    private static readonly JsonSerializerOptions Options = new()
    {
        ReadCommentHandling = JsonCommentHandling.Skip,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        AllowTrailingCommas = true,
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };

    public static void ApplyOptions(JsonSerializerOptions value)
    {
        value.ReadCommentHandling = Options.ReadCommentHandling;
        value.PropertyNameCaseInsensitive = Options.PropertyNameCaseInsensitive;
        value.DefaultIgnoreCondition = Options.DefaultIgnoreCondition;
        value.PropertyNamingPolicy = Options.PropertyNamingPolicy;
        value.AllowTrailingCommas = Options.AllowTrailingCommas;

        foreach (var converter in Options.Converters)
        {
            value.Converters.Add(converter);
        }
    }

    public static string Serialize(object obj)
    {
        return JsonSerializer.Serialize(obj, Options);
    }

    public static Task SerializeAsync<T>(Stream stream, T value)
    {
        return JsonSerializer.SerializeAsync(stream, value, options: Options);
    }

    public static T? Deserialize<T>(string json)
    {
        return string.IsNullOrEmpty(json) ? default : JsonSerializer.Deserialize<T>(json, Options);
    }

    public static object? Deserialize(string json, Type returnType)
    {
        return string.IsNullOrEmpty(json) ? default : JsonSerializer.Deserialize(json, returnType, Options);
    }

    public static ValueTask<T?> DeserializeAsync<T>(Stream stream)
    {
        return JsonSerializer.DeserializeAsync<T>(stream, options: Options);
    }
}