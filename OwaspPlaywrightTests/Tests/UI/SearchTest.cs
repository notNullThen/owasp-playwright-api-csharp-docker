using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Data;
using OwaspPlaywrightTests.Pages;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests.UI;

public class SearchTest(ITestOutputHelper output) : Test(output)
{
    [Fact]
    public async Task UserCanSearchForItemName()
    {
        var homePage = new HomePage();
        var partialName = ProductsData.RaspberryJuice.Name.Split(' ')[0];

        await homePage.GoToAsync();
        await homePage.Header.SearchBar.SearchAsync(partialName);

        var productTile = homePage.ProductTiles.GetByName(ProductsData.RaspberryJuice.Name);
        await Expect(productTile.Body).ToBeVisibleAsync();
    }
}
