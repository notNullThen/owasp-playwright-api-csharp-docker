using System.Text.Json.Serialization;
using OwaspPlaywrightTests.ApiEndpoints.Types.BasketItems;

namespace OwaspPlaywrightTests.ApiEndpoints.Types.Products;

public class ProductData
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("price")]
    public required decimal Price { get; set; }

    [JsonPropertyName("deluxePrice")]
    public required decimal DeluxePrice { get; set; }

    [JsonPropertyName("image")]
    public required string Image { get; set; }

    [JsonPropertyName("createdAt")]
    public required string CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public required string UpdatedAt { get; set; }

    [JsonPropertyName("deletedAt")]
    public string? DeletedAt { get; set; }

    [JsonPropertyName("BasketItem")]
    public BasketItemData? BasketItem { get; set; }
}
