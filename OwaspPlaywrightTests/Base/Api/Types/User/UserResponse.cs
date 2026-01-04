using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.Base.Api.Types.User;

public class UserResponse
{
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("data")]
    public required Data Data { get; set; }
};
