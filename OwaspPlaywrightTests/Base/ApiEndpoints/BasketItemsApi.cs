using OwaspPlaywrightTests.Base.ApiHandler;
using OwaspPlaywrightTests.Base.ApiHandler.Types;

namespace OwaspPlaywrightTests.Base.ApiEndpoints;

public class BasketItemsApi() : ApiBase("api/BasketItems")
{
    public ApiAction<dynamic> PostBasketItems() => Action(new() { Method = ApiHttpMethod.POST });
}
