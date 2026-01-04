using OwaspPlaywrightTests.Base.Api.Base;
using OwaspPlaywrightTests.Base.Api.Base.Types;
using OwaspPlaywrightTests.Base.Api.Types.RestUserApi;

namespace OwaspPlaywrightTests.Base.Api;

public class RestUserApi() : ApiBase("rest/user")
{
    public ApiAction<LoginResponse> PostLogin() =>
        Action<LoginResponse>(new() { Method = ApiHttpMethod.POST });
}
