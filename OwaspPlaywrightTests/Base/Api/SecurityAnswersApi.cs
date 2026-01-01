using OwaspPlaywrightTests.Base.Api.Base;
using OwaspPlaywrightTests.Base.Api.Base.Types;
using OwaspPlaywrightTests.Base.Api.Base.Types.SecurityAnswers;

namespace OwaspPlaywrightTests.Base.Api;

public class SecurityAnswersApi() : ApiBase("api/SecurityAnswers")
{
    public ApiAction<SecurityQuestionResponse> PostSecurityAnswers(
        SecurityAnswersPayload payload
    ) => Action<SecurityQuestionResponse>(new() { Method = ApiHttpMethod.POST, Body = payload });
}
