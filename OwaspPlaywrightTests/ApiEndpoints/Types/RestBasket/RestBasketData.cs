using System.Text.Json.Serialization;
using OwaspPlaywrightTests.ApiEndpoints.Types.Products;

namespace OwaspPlaywrightTests.ApiEndpoints.Types.RestBasket;

public class RestBasketData
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("coupon")]
    public string? Coupon { get; set; }

    [JsonPropertyName("UserId")]
    public required int UserId { get; set; }

    [JsonPropertyName("createdAt")]
    public required string CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public required string UpdatedAt { get; set; }

    [JsonPropertyName("Products")]
    public required List<ProductData> Products { get; set; }
}
