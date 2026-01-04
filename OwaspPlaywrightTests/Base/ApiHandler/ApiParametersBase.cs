using OwaspPlaywrightTests.Base.ApiHandler.Types;
using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests.Base.ApiHandler;

public abstract class ApiParametersBase(string baseApiUrl)
{
    protected int ApiWaitTimeout = TestConfig.ApiWaitTimeout;
    protected string FullUrl = string.Empty;
    protected string? Route;
    protected ApiHttpMethod Method;
    protected int[] ExpectedStatusCodes = TestConfig.ExpectedAPIResponseCodes;
    protected object? Body;
    private readonly string _baseApiUrl = TestUtils.ConnectUrlParts(TestConfig.BaseURL, baseApiUrl);

    protected ApiParametersBase AquireParameters(RequestParameters parameters)
    {
        FullUrl = TestUtils.ConnectUrlParts(_baseApiUrl, parameters.Url ?? string.Empty);
        Route = FullUrl.Replace(TestUtils.ConnectUrlParts(TestConfig.BaseURL), "");
        Method = parameters.Method;
        Body = parameters.Body;

        return this;
    }
}
