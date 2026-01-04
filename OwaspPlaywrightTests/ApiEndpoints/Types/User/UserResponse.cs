using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.ApiEndpoints.Types.User;

public class UserResponse
{
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("data")]
    public required UserData Data { get; set; }
};
