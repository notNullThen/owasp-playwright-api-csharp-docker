using OwaspPlaywrightTests.ApiEndpoints;
using OwaspPlaywrightTests.ApiEndpoints.Types.Products;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Support.Helpers;

public static class ProductsHelper
{
    public static async Task<ProductData> GetProductByNameAsync(string name)
    {
        return await Test.StepAsync(
            $"Get product with name \"{name}\"",
            async () =>
            {
                var searchResults = await Api.Products.SearchProducts(name).RequestAsync();
                return searchResults.ResponseBody!.Data.First(p =>
                    p.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)
                );
            }
        );
    }
}
