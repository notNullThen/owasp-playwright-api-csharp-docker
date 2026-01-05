using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.ApiEndpoints.Types.RestUserApi;

public class Authentication
{
    [JsonPropertyName("token")]
    public required string Token { get; set; }

    [JsonPropertyName("bid")]
    public required int Bid { get; set; }

    [JsonPropertyName("umail")]
    public required string Umail { get; set; }
}
