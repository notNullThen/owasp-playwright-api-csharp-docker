using OwaspPlaywrightTests.ApiEndpoints.Types.BasketItems;
using OwaspPlaywrightTests.Base.ApiClient;
using OwaspPlaywrightTests.Base.ApiClient.Types;

namespace OwaspPlaywrightTests.ApiEndpoints;

public class BasketItemsApi() : ApiEndpointBase("api/BasketItems")
{
    public ApiAction<BasketItemsResponse> PostBasketItems(BasketItemsPayload? payload = null) =>
        Action<BasketItemsResponse>(new() { Method = ApiHttpMethod.POST, Body = payload });

    public ApiAction<object> DeleteBasketItems(string? userId = null) =>
        Action<object>(new() { Method = ApiHttpMethod.DELETE, Url = userId });
}
