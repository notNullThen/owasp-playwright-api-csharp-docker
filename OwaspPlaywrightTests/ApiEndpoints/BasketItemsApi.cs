using OwaspPlaywrightTests.ApiEndpoints.Types.BasketItems;
using SimpleApiPlaywright;
using SimpleApiPlaywright.Types;

namespace OwaspPlaywrightTests.ApiEndpoints;

public class BasketItemsApi() : ApiEndpointBase("api/BasketItems")
{
    public ApiAction<BasketItemsResponse> PostBasketItems(BasketItemsPayload? payload = null) =>
        Action<BasketItemsResponse>(new() { Method = ApiHttpMethod.POST, Body = payload });

    public ApiAction<object> DeleteBasketItems(string? userId = null) =>
        Action<object>(new() { Method = ApiHttpMethod.DELETE, Url = userId });
}
