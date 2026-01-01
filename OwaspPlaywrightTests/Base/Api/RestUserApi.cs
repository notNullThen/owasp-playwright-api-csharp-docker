using OwaspPlaywrightTests.Base.Api.Base;
using OwaspPlaywrightTests.Base.Api.Base.Types;
using OwaspPlaywrightTests.Base.Api.Base.Types.RestUserApi;

namespace OwaspPlaywrightTests.Base.Api;

public class RestUserApi() : ApiBase("rest/user")
{
    public ApiAction<LoginResponse> Login() =>
        Action<LoginResponse>(new() { Method = ApiHttpMethod.POST });
}
