using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.Base.ApiEndpoints.Types.RestUserApi;

public class Authentication
{
    [JsonPropertyName("token")]
    public required string Token;

    [JsonPropertyName("bid")]
    public required string Bid;

    [JsonPropertyName("email")]
    public required string Email;
}
