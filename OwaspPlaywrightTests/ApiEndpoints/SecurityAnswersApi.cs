using OwaspPlaywrightTests.ApiEndpoints.Types;
using OwaspPlaywrightTests.ApiEndpoints.Types.SecurityAnswers;
using OwaspPlaywrightTests.Base.ApiHandler;
using OwaspPlaywrightTests.Base.ApiHandler.Types;

namespace OwaspPlaywrightTests.ApiEndpoints;

public class SecurityAnswersApi() : ApiEndpointBase("api/SecurityAnswers")
{
    public ApiAction<SecurityAnswerResponse> PostSecurityAnswers(
        SecurityAnswersPayload? payload = null
    ) => Action<SecurityAnswerResponse>(new() { Method = ApiHttpMethod.POST, Body = payload });
}
