using OwaspPlaywrightTests.Base.Api.Base;
using OwaspPlaywrightTests.Base.Api.Base.Types;
using OwaspPlaywrightTests.Base.Api.Types.User;

namespace OwaspPlaywrightTests.Base.Api;

public class UsersApi() : ApiBase("api/Users")
{
    public ApiAction<UserResponse> PostUser(UserPayload? payload = null) =>
        Action<UserResponse>(new() { Method = ApiHttpMethod.POST, Body = payload });
}
