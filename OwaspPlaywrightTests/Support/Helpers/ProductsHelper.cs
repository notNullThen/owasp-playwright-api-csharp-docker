using OwaspPlaywrightTests.ApiEndpoints;
using OwaspPlaywrightTests.ApiEndpoints.Types.RestBasket;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Support.Helpers;

public static class ProductsHelper
{
    public static async Task<RestBasketProductData> GetProductByNameAsync(string name)
    {
        return await Test.StepAsync(
            $"Get product with name \"{name}\"",
            async () =>
            {
                var searchResults = await Api.Products.GetSearchProducts(name).RequestAsync();
                return searchResults.ResponseBody!.Data.First(product =>
                    product.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)
                );
            }
        );
    }
}
