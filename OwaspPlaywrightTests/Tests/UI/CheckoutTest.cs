using OwaspPlaywrightTests.Data;
using OwaspPlaywrightTests.Hooks;
using OwaspPlaywrightTests.Pages;
using OwaspPlaywrightTests.Support.Helpers;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests.UI;

public class CheckoutTest(ITestOutputHelper outputHelper) : AuthenticatedHook(outputHelper)
{
    [Fact]
    public async Task UserCanPerformCheckout()
    {
        var homePage = new HomePage();

        var productData = ProductsData.BananaJuice;
        var searchQuery = productData.Name.Split(' ')[0].ToLower();
        var product = await ProductsHelper.GetProductByNameAsync(searchQuery);
        var basketId = LoginResponseBody.Authentication.Bid;

        await BasketHelper.AddProductToBasketAsync(
            basketId: basketId,
            productId: product.Id,
            quantity: 1
        );

        await homePage.GotoAsync();
    }
}
