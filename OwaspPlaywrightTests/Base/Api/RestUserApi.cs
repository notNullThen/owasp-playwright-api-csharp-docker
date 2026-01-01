using System.Text.Json.Serialization;
using OwaspPlaywrightTests.Base.Api.Base;

namespace OwaspPlaywrightTests.Base.Api;

public class RestUserApi() : ApiBase("rest/user")
{
    public class Authentication
    {
        [JsonPropertyName("token")]
        public required string Token;

        [JsonPropertyName("bid")]
        public required string Bid;

        [JsonPropertyName("email")]
        public required string Email;
    }

    public class LoginResponse
    {
        [JsonPropertyName("authentication")]
        public required Authentication authentication;
    }

    public ApiAction<LoginResponse> Login() =>
        Action<LoginResponse>(new() { Method = Types.ApiHttpMethod.POST });
}
