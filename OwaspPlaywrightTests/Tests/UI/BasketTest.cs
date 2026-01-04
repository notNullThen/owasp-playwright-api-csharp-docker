using OwaspPlaywrightTests.Data;
using OwaspPlaywrightTests.Hooks;
using OwaspPlaywrightTests.Pages;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests.UI;

public class BasketTest(ITestOutputHelper output) : AuthenticatedHook(output)
{
    [Fact]
    public async Task UserCanSearchAndAddProductToBasket()
    {
        var homePage = new HomePage();
        var basketPage = new BasketPage();

        var product = ProductsData.BananaJuice;
        var partialName = product.Name.Split(' ')[0];

        await homePage.GotoAsync();
        await homePage.Header.SearchBar.SearchAsync(partialName);
        await homePage.ProductTiles.GetByName(product.Name).AddToBasketAsync();

        await basketPage.GotoAsync();
        var expectedProductRow = await basketPage.Products.GetByName(product.Name);
        await Expect(expectedProductRow.Body).ToBeVisibleAsync();
    }
}
