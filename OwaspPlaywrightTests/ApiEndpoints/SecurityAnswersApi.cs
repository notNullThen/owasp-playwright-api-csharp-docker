using OwaspPlaywrightTests.ApiEndpoints.Types;
using OwaspPlaywrightTests.ApiEndpoints.Types.SecurityAnswers;
using SimpleApiPlaywright;
using SimpleApiPlaywright.Types;

namespace OwaspPlaywrightTests.ApiEndpoints;

public class SecurityAnswersApi() : ApiEndpointBase("api/SecurityAnswers")
{
    public ApiAction<SecurityAnswerResponse> PostSecurityAnswers(
        SecurityAnswersPayload? payload = null
    ) => Action<SecurityAnswerResponse>(new() { Method = ApiHttpMethod.POST, Body = payload });
}
