using OwaspPlaywrightTests.Base.ApiHandler.Types;
using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests.Base.ApiHandler;

public abstract class ApiParametersBase(string baseApiUrl)
{
    protected int ApiWaitTimeout = PlaywrightConfig.ApiWaitTimeout;
    protected string FullUrl = string.Empty;
    protected string? Route;
    protected ApiHttpMethod Method;
    protected int[] ExpectedStatusCodes = PlaywrightConfig.ExpectedAPIResponseCodes;
    protected object? Body;
    private readonly string _baseApiUrl = TestUtils.ConnectUrlParts(
        PlaywrightConfig.BaseURL,
        baseApiUrl
    );

    protected ApiParametersBase AquireParameters(RequestParameters parameters)
    {
        FullUrl = TestUtils.ConnectUrlParts(_baseApiUrl, parameters.Url ?? string.Empty);
        Route = FullUrl.Replace(TestUtils.ConnectUrlParts(PlaywrightConfig.BaseURL), "");
        Method = parameters.Method;
        Body = parameters.Body;

        return this;
    }
}
