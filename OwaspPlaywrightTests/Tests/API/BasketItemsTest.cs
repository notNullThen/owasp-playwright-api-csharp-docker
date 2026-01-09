using OwaspPlaywrightTests.ApiEndpoints;
using OwaspPlaywrightTests.Hooks;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests.API;

public class BasketItemsTest(ITestOutputHelper outputHelper) : AuthenticatedHook(outputHelper)
{
    [Fact(DisplayName = "Review: User can add item to basket")]
    public async Task UserCanAddItemToBasket()
    {
        var productsResponse = await Api.Products.GetSearchProducts("").RequestAsync();

        Assert.NotNull(productsResponse.ResponseBody?.Data);
        Assert.NotEmpty(productsResponse.ResponseBody.Data);
        var productToAdd = productsResponse.ResponseBody.Data.First();

        Assert.True(productToAdd.Id > 0, "Product ID should be valid");

        var basketId = LoggedInUserResponse.Authentication.Bid;
        Assert.True(basketId > 0, "Basket ID (from login) should be valid");

        var addResponse = await Api
            .BasketItems.PostBasketItems(
                new()
                {
                    BasketId = basketId.ToString(),
                    ProductId = productToAdd.Id,
                    Quantity = 1,
                }
            )
            .RequestAsync();

        Assert.NotNull(addResponse.ResponseBody);
        var addedItem = addResponse.ResponseBody.Data;

        Assert.Equal("success", addResponse.ResponseBody.Status);

        Assert.NotNull(addedItem);
        Assert.True(addedItem.Id > 0, "Added item ID should be valid");
        Assert.Equal(productToAdd.Id, addedItem.ProductId);
        Assert.Equal(basketId.ToString(), addedItem.BasketId);
        Assert.Equal(1, addedItem.Quantity);
    }
}
