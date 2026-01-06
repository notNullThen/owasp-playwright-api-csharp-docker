using OwaspPlaywrightTests.ApiEndpoints.Types.RestBasket;
using OwaspPlaywrightTests.Base.ApiHandler;
using OwaspPlaywrightTests.Base.ApiHandler.Types;

namespace OwaspPlaywrightTests.ApiEndpoints;

public class RestBasketApi() : ApiEndpointBase("rest/basket")
{
    public ApiAction<RestBasketResponse> GetBasket(string? userId = null) =>
        Action<RestBasketResponse>(
            new()
            {
                Url = string.IsNullOrWhiteSpace(userId) ? null : userId,
                Method = ApiHttpMethod.GET,
            }
        );
}
