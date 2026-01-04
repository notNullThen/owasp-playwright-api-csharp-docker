using Microsoft.Playwright;
using OwaspPlaywrightTests.ApiEndpoints;
using OwaspPlaywrightTests.ApiEndpoints.Types.BasketItems;
using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Base.ApiHandler.Types;
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

    public async Task<BrowserApiResponse<BasketItemsResponse>> AddToBasketAsync()
    {
        return await Test.StepAsync(
            $"Add product '{await ItemName.InnerTextAsync()}' to basket",
            async () =>
            {
                var basketItemsWaitTask = Api.BasketItems.PostBasketItems().WaitAsync();
                await Task.WhenAll(basketItemsWaitTask, AddToBasketButton.ClickAsync());
                return await basketItemsWaitTask;
            }
        );
    }
}
