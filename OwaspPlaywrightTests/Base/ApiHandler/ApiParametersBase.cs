using OwaspPlaywrightTests.Base.ApiHandler.Types;

namespace OwaspPlaywrightTests.Base.ApiHandler;

public abstract class ApiParametersBase(string baseApiUrl)
{
    protected static int s_apiWaitTimeout;
    private static IReadOnlyCollection<int> s_defaultExpectedStatusCodes = null!;
    protected IReadOnlyCollection<int> _expectedStatusCodes = null!;
    protected static string s_baseUrl = null!;

    protected string _fullUrl = string.Empty;
    protected string? _route;
    protected ApiHttpMethod _method;
    protected object? _body;
    private readonly string _baseApiUrl = ConnectUrlParts(s_baseUrl, baseApiUrl);

    protected ApiParametersBase AquireParameters(RequestParameters parameters)
    {
        if (s_apiWaitTimeout == 0 || s_defaultExpectedStatusCodes == null || s_baseUrl == null)
        {
            throw new InvalidOperationException(
                $"You need to set initial config (usually in Tests Global Setup) via '{nameof(SetInitialConfig)}' method before using '{nameof(ApiParametersBase)}' class."
            );
        }

        _fullUrl = ConnectUrlParts(_baseApiUrl, parameters.Url ?? string.Empty);
        _route = _fullUrl.Replace(ConnectUrlParts(s_baseUrl), "");
        _method = parameters.Method;
        _expectedStatusCodes = parameters.ExpectedStatusCodes ?? s_defaultExpectedStatusCodes;
        _body = parameters.Body;

        return this;
    }

    public static void SetToken(string token)
    {
        Test.ApiToken = token;
    }

    protected static string? GetToken() => Test.ApiToken;

    public static void SetInitialConfig(
        int apiWaitTimeout,
        IReadOnlyCollection<int> expectedStatusCodes,
        string baseUrl
    )
    {
        s_apiWaitTimeout = apiWaitTimeout;
        s_defaultExpectedStatusCodes = expectedStatusCodes;
        s_baseUrl = baseUrl;
    }

    protected static string ConnectUrlParts(params string[] parts)
    {
        var connectedParts = string.Join(
            "/",
            parts
                .Where(part => !string.IsNullOrEmpty(part))
                .Select(NormalizeUrl)
                .Where(part => part.Trim().Length > 0)
        );

        return connectedParts;
    }

    protected static string NormalizeUrl(string url)
    {
        return RemoveLeadingSlash(RemoveTrailingSlash(url));
    }

    private static string RemoveTrailingSlash(string url)
    {
        return url.EndsWith('/') ? url.Substring(0, url.Length - 1) : url;
    }

    private static string RemoveLeadingSlash(string url)
    {
        return url.StartsWith('/') ? url.Substring(1) : url;
    }
}
