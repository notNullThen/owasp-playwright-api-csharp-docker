using Microsoft.Playwright;
using OwaspPlaywrightTests.ApiEndpoints;
using OwaspPlaywrightTests.ApiEndpoints.Types.BasketItems;
using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Base.ApiClient.Types;
using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests.Components;

public class ProductTile()
    : IterableComponentBase(componentName: "Product tile", body: Test.Page.Locator("mat-grid-tile"))
{
    private const string ItemNameSelector = ".item-name";

    public ILocator ItemName => Body.Locator(ItemNameSelector);
    public ILocator Price => Body.Locator(".item-price");
    public ILocator AddToBasketButton => Body.Locator(".btn-basket");

    public async Task<float> GetPriceValueAsync()
    {
        var priceText = await Price.InnerTextAsync();
        return TestUtils.GetPriceFromText(priceText);
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

    public override ProductTile GetByText(string text) => GetByTextBase<ProductTile>(text);

    public override ProductTile GetByIndex(int index) => GetByIndexBase<ProductTile>(index);

    protected override ProductTile Create(ILocator body) => new() { Body = body };
}
