using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests.Components;

public class ProductTile()
    : ComponentBase(componentName: "Product tile", body: Test.Page.Locator("mat-grid-tile"))
{
    private const string ItemNameSelector = ".item-name";

    public ILocator ItemName => Body.Locator(ItemNameSelector);
    public ILocator Price => Body.Locator(".item-price");
    public ILocator AddToBasketButton => Body.Locator(".btn-basket");

    public async Task<float> GetPriceValueAsync()
    {
        var priceText = await Price.InnerTextAsync();
        return Utils.GetPriceFromText(priceText);
    }

    public ProductTile GetByName(string name)
    {
        var tile = new ProductTile();
        tile.Body = Body.Filter(new() { Has = Page.Locator(ItemNameSelector).GetByText(name) });
        return tile;
    }
}
