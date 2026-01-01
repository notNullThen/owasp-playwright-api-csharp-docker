using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.Base.Api.Base.Types.RestUserApi;

public class LoginResponse
{
    [JsonPropertyName("authentication")]
    public required Authentication authentication;
}
