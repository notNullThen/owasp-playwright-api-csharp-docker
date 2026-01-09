using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Data;
using OwaspPlaywrightTests.Pages;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests.UI;

[Trait("Suite", "UI")]
public class SearchTest(ITestOutputHelper output) : Test(output)
{
    [Fact(DisplayName = "User can search for item by name")]
    public async Task UserCanSearchForItemName()
    {
        var homePage = new HomePage();
        var partialName = ProductsTestData.RaspberryJuice.Name.Split(' ')[0];

        await homePage.GotoAsync();
        await homePage.Header.SearchBar.SearchAsync(partialName);

        var productTile = homePage.ProductTiles.GetByName(ProductsTestData.RaspberryJuice.Name);
        await Expect(productTile.Body).ToBeVisibleAsync();
    }
}
