using OwaspPlaywrightTests.ApiEndpoints.Types.RestBasket;
using OwaspPlaywrightTests.Base.ApiClient;
using OwaspPlaywrightTests.Base.ApiClient.Types;

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
