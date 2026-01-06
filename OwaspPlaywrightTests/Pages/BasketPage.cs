using Microsoft.Playwright;
using OwaspPlaywrightTests.ApiEndpoints;
using OwaspPlaywrightTests.ApiEndpoints.Types.RestBasket;
using OwaspPlaywrightTests.Base.ApiHandler.Types;
using OwaspPlaywrightTests.Components;
using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests.Pages;

public class BasketPage() : PageBase("#/basket")
{
    public ProductRow Products => new();

    public new async Task<BrowserApiResponse<RestBasketResponse>> GotoAsync()
    {
        var basketWaitTask = Api.RestBasket.GetBasket().WaitAsync();

        await Task.WhenAll(base.GotoAsync(), basketWaitTask);

        return await basketWaitTask;
    }

    public ILocator Price => Page.Locator("#price");

    public async Task<float> GetTotalPriceValueAsync()
    {
        var priceText = await Price.InnerTextAsync();
        var priceWithoutPrefix = priceText.Replace("Total Price:", "").Trim();
        return TestUtils.GetPriceFromText(priceWithoutPrefix);
    }
}
