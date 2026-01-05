using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.ApiEndpoints.Types;

public abstract class ResponseBase<T>
{
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("data")]
    public required T Data { get; set; }
}
