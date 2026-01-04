using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.ApiEndpoints.Types.RestUserApi;

public class LoginResponse
{
    [JsonPropertyName("authentication")]
    public required Authentication authentication;
}
