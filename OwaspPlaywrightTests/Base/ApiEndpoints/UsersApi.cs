using OwaspPlaywrightTests.Base.ApiEndpoints.Types.User;
using OwaspPlaywrightTests.Base.ApiHandler;
using OwaspPlaywrightTests.Base.ApiHandler.Types;

namespace OwaspPlaywrightTests.Base.ApiEndpoints;

public class UsersApi() : ApiEndpointBase("api/Users")
{
    public ApiAction<UserResponse> PostUser(UserPayload? payload = null) =>
        Action<UserResponse>(new() { Method = ApiHttpMethod.POST, Body = payload });
}
