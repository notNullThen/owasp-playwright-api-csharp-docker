using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.ApiEndpoints.Types.BasketItems;

public class BasketItemData
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("ProductId")]
    public required int ProductId { get; set; }

    [JsonPropertyName("BasketId")]
    public required int BasketId { get; set; }

    [JsonPropertyName("quantity")]
    public required int Quantity { get; set; }

    [JsonPropertyName("updatedAt")]
    public required string UpdatedAt { get; set; }

    [JsonPropertyName("createdAt")]
    public required string CreatedAt { get; set; }
}
