using OwaspPlaywrightTests.Base.Api.Base;

namespace OwaspPlaywrightTests.Base.Api;

public class BasketItemsApi() : ApiBase("api/BasketItems")
{
    public ApiAction<dynamic> PostBasketItems() =>
        Action(new() { Method = Base.Types.ApiHttpMethod.POST });
}
