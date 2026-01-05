using OwaspPlaywrightTests.ApiEndpoints;
using OwaspPlaywrightTests.ApiEndpoints.Types.BasketItems;
using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Base.ApiHandler.Types;

namespace OwaspPlaywrightTests.Support.Helpers;

public static class BasketHelper
{
    public static async Task<ApiResponse<BasketItemsResponse>> AddProductToBasketAsync(
        string basketId,
        int productId,
        int quantity
    )
    {
        return await AddProductToBasketAsync(int.Parse(basketId), productId, quantity);
    }

    public static async Task<ApiResponse<BasketItemsResponse>> AddProductToBasketAsync(
        int basketId,
        int productId,
        int quantity
    )
    {
        return await Test.StepAsync(
            $"Add product with ID {productId} to basket {basketId}",
            async () =>
                await Api
                    .BasketItems.PostBasketItems(
                        new()
                        {
                            BasketId = basketId.ToString(),
                            ProductId = productId,
                            Quantity = quantity,
                        }
                    )
                    .RequestAsync()
        );
    }
}
