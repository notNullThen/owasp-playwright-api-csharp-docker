using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.Base.Api.Types.RestUserApi;

public class LoginResponse
{
    [JsonPropertyName("authentication")]
    public required Authentication authentication;
}
