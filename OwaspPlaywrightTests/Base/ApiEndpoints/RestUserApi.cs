using OwaspPlaywrightTests.Base.ApiEndpoints.Types.RestUserApi;
using OwaspPlaywrightTests.Base.ApiHandler;
using OwaspPlaywrightTests.Base.ApiHandler.Types;

namespace OwaspPlaywrightTests.Base.ApiEndpoints;

public class RestUserApi() : ApiBase("rest/user")
{
    public ApiAction<LoginResponse> PostLogin() =>
        Action<LoginResponse>(new() { Method = ApiHttpMethod.POST });
}
