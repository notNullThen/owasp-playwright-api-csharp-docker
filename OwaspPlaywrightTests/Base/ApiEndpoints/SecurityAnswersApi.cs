using OwaspPlaywrightTests.Base.ApiEndpoints.Types.SecurityAnswers;
using OwaspPlaywrightTests.Base.ApiHandler;
using OwaspPlaywrightTests.Base.ApiHandler.Types;

namespace OwaspPlaywrightTests.Base.ApiEndpoints;

public class SecurityAnswersApi() : ApiEndpointBase("api/SecurityAnswers")
{
    public ApiAction<SecurityQuestionResponse> PostSecurityAnswers(
        SecurityAnswersPayload? payload = null
    ) => Action<SecurityQuestionResponse>(new() { Method = ApiHttpMethod.POST, Body = payload });
}
