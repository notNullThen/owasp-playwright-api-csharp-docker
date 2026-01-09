using OwaspPlaywrightTests.ApiEndpoints;
using OwaspPlaywrightTests.Base;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests.API;

public class ProductsTest(ITestOutputHelper outputHelper) : Test(outputHelper)
{
    [Fact(DisplayName = "Can search for products")]
    public async Task CanSearchForProducts()
    {
        var response = await StepAsync(
            "Search for products with query 'apple'",
            async () => await Api.Products.GetSearchProducts("apple").RequestAsync()
        );

        Assert.NotNull(response.ResponseBody);
        Assert.Equal("success", response.ResponseBody.Status);
        Assert.NotNull(response.ResponseBody.Data);
        Assert.NotEmpty(response.ResponseBody.Data);

        foreach (var product in response.ResponseBody.Data)
        {
            Assert.Contains("apple", product.Name, StringComparison.InvariantCultureIgnoreCase);
        }
    }

    [Fact(DisplayName = "Searching for non-existent product returns empty list")]
    public async Task SearchForNonExistentProductReturnsEmptyList()
    {
        var response = await StepAsync(
            "Search for products with query 'non-existent-product-12345'",
            async () =>
                await Api.Products.GetSearchProducts("non-existent-product-12345").RequestAsync()
        );

        Assert.NotNull(response.ResponseBody);
        Assert.Equal("success", response.ResponseBody.Status);
        Assert.NotNull(response.ResponseBody.Data);
        Assert.Empty(response.ResponseBody.Data);
    }
}
