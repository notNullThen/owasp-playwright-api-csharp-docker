using OwaspPlaywrightTests.ApiEndpoints.Types;
using OwaspPlaywrightTests.ApiEndpoints.Types.Products;
using OwaspPlaywrightTests.Base.ApiClient;
using OwaspPlaywrightTests.Base.ApiClient.Types;

namespace OwaspPlaywrightTests.ApiEndpoints;

public class ProductsApi() : ApiEndpointBase("rest/products")
{
    public ApiAction<ProductsResponse> GetSearchProducts(string query)
    {
        var encodedQuery = Uri.EscapeDataString(query);
        return Action<ProductsResponse>(
            new() { Method = ApiHttpMethod.GET, Url = $"search?q={encodedQuery}" }
        );
    }
}
