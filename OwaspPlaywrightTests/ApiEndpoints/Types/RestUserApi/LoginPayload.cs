using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.ApiEndpoints.Types.RestUserApi;

public class LoginPayload
{
    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }
}
