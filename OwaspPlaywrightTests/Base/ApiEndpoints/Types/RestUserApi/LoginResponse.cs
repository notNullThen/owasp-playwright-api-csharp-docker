using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.Base.ApiEndpoints.Types.RestUserApi;

public class LoginResponse
{
    [JsonPropertyName("authentication")]
    public required Authentication authentication;
}
