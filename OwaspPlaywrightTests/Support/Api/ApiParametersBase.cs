using OwaspPlaywrightTests.Support.Api.Types;

namespace OwaspPlaywrightTests.Support.Api;

public abstract class ApiParametersBase(string baseApiUrl)
{
    protected int ApiWaitTimeout = PlaywrightConfig.ApiWaitTimeout;
    protected string FullUrl = string.Empty;
    protected string? Route;
    protected ApiHttpMethod Method;
    protected int[] ExpectedStatusCodes = [];
    protected RequestParameters Params = new();
    private readonly string _baseApiUrl = Utils.ConnectUrlParts(
        PlaywrightConfig.BaseURL,
        baseApiUrl
    );

    protected void AquireParameters(RequestParameters parameters)
    {
        FullUrl = Utils.ConnectUrlParts(_baseApiUrl, parameters.Url ?? string.Empty);
        Route = FullUrl.Replace(Utils.ConnectUrlParts(PlaywrightConfig.BaseURL), "");
        Method = parameters.Method;
        ExpectedStatusCodes =
            parameters.ExpectedStatusCodes ?? PlaywrightConfig.ExpectedAPIResponseCodes;
        Params = parameters;
    }
}
