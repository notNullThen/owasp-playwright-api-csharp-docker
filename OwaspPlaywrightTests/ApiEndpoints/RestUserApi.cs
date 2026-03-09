using OwaspPlaywrightTests.ApiEndpoints.Types.RestUserApi;
using OwaspPlaywrightTests.Base.ApiClient;
using OwaspPlaywrightTests.Base.ApiClient.Types;

namespace OwaspPlaywrightTests.ApiEndpoints;

public class RestUserApi() : ApiEndpointBase("rest/user")
{
    public ApiAction<LoginResponse> PostLogin(LoginPayload? payload = null) =>
        Action<LoginResponse>(
            new()
            {
                Url = "login",
                Method = ApiHttpMethod.POST,
                Body = payload,
            }
        );
}
