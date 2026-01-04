using OwaspPlaywrightTests.ApiEndpoints.Types;
using OwaspPlaywrightTests.ApiEndpoints.Types.BasketItems;
using OwaspPlaywrightTests.Base.ApiHandler;
using OwaspPlaywrightTests.Base.ApiHandler.Types;

namespace OwaspPlaywrightTests.ApiEndpoints;

public class BasketItemsApi() : ApiEndpointBase("api/BasketItems")
{
    public ApiAction<BasketItemsResponse> PostBasketItems(BasketItemsPayload? payload = null) =>
        Action<BasketItemsResponse>(new() { Method = ApiHttpMethod.POST, Body = payload });
}
