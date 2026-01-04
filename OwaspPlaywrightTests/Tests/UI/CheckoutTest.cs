using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Data;
using OwaspPlaywrightTests.Support.Helpers;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests.UI;

public class CheckoutTest(ITestOutputHelper outputHelper) : Test(outputHelper)
{
    [Fact]
    public async Task UserCanPerformCheckout()
    {
        var productData = ProductsData.BananaJuice;
        var product = await ProductsHelper.GetProductByNameAsync("banana");
    }
}
