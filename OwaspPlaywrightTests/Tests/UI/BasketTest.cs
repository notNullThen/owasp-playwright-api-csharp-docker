using OwaspPlaywrightTests.Data;
using OwaspPlaywrightTests.Hooks;
using OwaspPlaywrightTests.Pages;
using OwaspPlaywrightTests.Support.Helpers;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests.UI;

public class BasketTest(ITestOutputHelper output) : AuthenticatedUiHook(output)
{
    [Fact]
    public async Task UserCanSearchAndAddProductToBasket()
    {
        var homePage = new HomePage();
        var basketPage = new BasketPage();

        var product = ProductsTestData.BananaJuice;
        var partialName = product.Name.Split(' ')[0];

        await homePage.GotoAsync();
        await homePage.Header.SearchBar.SearchAsync(partialName);
        await homePage.ProductTiles.GetByName(product.Name).AddToBasketAsync();

        await basketPage.GotoAsync();
        var expectedProduct = await basketPage.Products.GetByNameAsync(product.Name);
        await Expect(expectedProduct.Body).ToBeVisibleAsync();
    }

    [Fact]
    public async Task UserCanPerformCheckout()
    {
        var basketPage = new BasketPage();

        var product = ProductsTestData.BananaJuice;
        var searchQuery = product.Name.Split(' ')[0].ToLower();
        var productResponse = await ProductsHelper.GetProductByNameAsync(searchQuery);
        var basketId = LoggedInUserResponse.Authentication.Bid;
        var quantity = 2;

        await BasketHelper.AddProductToBasketAsync(
            basketId: basketId,
            productId: productResponse.Id,
            quantity: quantity
        );

        await basketPage.GotoAsync();
        var row = await basketPage.Products.GetByNameAsync(product.Name);

        var rowProductName = await row.GetProductNameAsync();
        Assert.Equal(rowProductName, product.Name);

        var rowQuantity = await row.GetQuantityValueAsync();
        Assert.Equal(rowQuantity, quantity);

        var rowPrice = await row.GetPriceValueAsync();
        Assert.Equal(rowPrice, product.Price);

        var totalPrice = await basketPage.GetTotalPriceValueAsync();
        Assert.Equal(product.Price * quantity, totalPrice);
    }
}
