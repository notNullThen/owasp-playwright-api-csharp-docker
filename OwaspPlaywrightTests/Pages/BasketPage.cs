using OwaspPlaywrightTests.ApiEndpoints;
using OwaspPlaywrightTests.ApiEndpoints.Types.RestBasket;
using OwaspPlaywrightTests.Base.ApiHandler.Types;
using OwaspPlaywrightTests.Components;

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
}
