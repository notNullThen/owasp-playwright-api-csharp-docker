using OwaspPlaywrightTests.ApiEndpoints.Types;
using OwaspPlaywrightTests.ApiEndpoints.Types.User;
using SimpleApiPlaywright;
using SimpleApiPlaywright.Types;

namespace OwaspPlaywrightTests.ApiEndpoints;

public class UsersApi() : ApiEndpointBase("api/Users")
{
    public ApiAction<UserResponse> PostUser(UserPayload? payload = null) =>
        Action<UserResponse>(new() { Method = ApiHttpMethod.POST, Body = payload });
}
