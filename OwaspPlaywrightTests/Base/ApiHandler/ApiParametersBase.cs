using OwaspPlaywrightTests.Base.ApiHandler.Types;
using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests.Base.ApiHandler;

public abstract class ApiParametersBase(string baseApiUrl)
{
    protected int _apiWaitTimeout = TestConfig.ApiWaitTimeout;
    protected string _fullUrl = string.Empty;
    protected string? _route;
    protected ApiHttpMethod _method;
    protected IReadOnlyCollection<int> _expectedStatusCodes = TestConfig.ExpectedApiStatusCodes;
    protected object? _body;
    private readonly string _baseApiUrl = TestUtils.ConnectUrlParts(TestConfig.BaseUrl, baseApiUrl);

    protected ApiParametersBase AquireParameters(RequestParameters parameters)
    {
        _fullUrl = TestUtils.ConnectUrlParts(_baseApiUrl, parameters.Url ?? string.Empty);
        _route = _fullUrl.Replace(TestUtils.ConnectUrlParts(TestConfig.BaseUrl), "");
        _method = parameters.Method;
        _body = parameters.Body;

        return this;
    }
}
