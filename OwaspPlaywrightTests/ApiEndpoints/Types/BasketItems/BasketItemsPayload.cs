using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.ApiEndpoints.Types.BasketItems;

public class BasketItemsPayload
{
    [JsonPropertyName("ProductId")]
    public required int ProductId { get; set; }

    [JsonPropertyName("BasketId")]
    public required string BasketId { get; set; }

    [JsonPropertyName("quantity")]
    public required int Quantity { get; set; }
}
