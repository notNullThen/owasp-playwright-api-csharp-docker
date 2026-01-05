using OwaspPlaywrightTests.ApiEndpoints.Types.RestUserApi;
using OwaspPlaywrightTests.Base.ApiHandler;
using OwaspPlaywrightTests.Base.ApiHandler.Types;

namespace OwaspPlaywrightTests.ApiEndpoints;

public class RestUserApi() : ApiEndpointBase("rest/user")
{
    public ApiAction<LoginResponse> PostLogin() =>
        Action<LoginResponse>(new() { Url = "login", Method = ApiHttpMethod.POST });
}
