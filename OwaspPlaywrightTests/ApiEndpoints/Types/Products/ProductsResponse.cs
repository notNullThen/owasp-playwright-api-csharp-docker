using System.Text.Json.Serialization;

namespace OwaspPlaywrightTests.ApiEndpoints.Types.Products;

public class ProductsResponse
{
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("data")]
    public required List<ProductData> Data { get; set; }
}
