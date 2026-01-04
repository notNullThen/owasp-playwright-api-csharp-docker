using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.ApiEndpoints.Types.BasketItems;

public class BasketItemsResponse
{
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("data")]
    public required BasketItemData Data { get; set; }
}
