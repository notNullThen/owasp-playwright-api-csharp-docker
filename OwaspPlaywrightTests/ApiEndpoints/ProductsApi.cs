using OwaspPlaywrightTests.ApiEndpoints.Types;
using OwaspPlaywrightTests.ApiEndpoints.Types.Products;
using OwaspPlaywrightTests.Base.ApiHandler;
using OwaspPlaywrightTests.Base.ApiHandler.Types;

namespace OwaspPlaywrightTests.ApiEndpoints;

public class ProductsApi() : ApiEndpointBase("rest/products")
{
    public ApiAction<ProductsResponse> SearchProducts(string query) =>
        Action<ProductsResponse>(new() { Method = ApiHttpMethod.GET, Url = $"search?q={query}" });
}
