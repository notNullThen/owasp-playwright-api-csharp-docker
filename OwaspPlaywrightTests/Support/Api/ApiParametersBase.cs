using System;
using System.Runtime.Serialization;
using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests.Support.Api;

public enum HttpMethod
{
    [EnumMember(Value = "GET")]
    GET,

    [EnumMember(Value = "POST")]
    POST,

    [EnumMember(Value = "PUT")]
    PUT,

    [EnumMember(Value = "DELETE")]
    DELETE,

    [EnumMember(Value = "HEAD")]
    HEAD,

    [EnumMember(Value = "PATCH")]
    PATCH,
}

public class RequestParameters
{
    public string? Url { get; set; }
    public HttpMethod Method;
    public int[]? ExpectedStatusCodes { get; set; }
    public object? Body { get; set; }
}

public abstract class ApiParametersBase(string baseApiUrl)
{
    protected int ApiWaitTimeout = PlaywrightConfig.ApiWaitTimeout;
    protected string? FullUrl;
    protected string? Route;
    protected HttpMethod Method;
    protected int[]? ExpectedStatusCodes;
    protected RequestParameters? Params;
    private readonly string _baseApiUrl = Utils.ConnectUrlParts(
        PlaywrightConfig.BaseURL,
        baseApiUrl
    );

    protected ApiParametersBase AquireParameters(RequestParameters parameters)
    {
        // Cloning the current instance to avoid racing conditions when calling API endpoints in parallel
        var clone = (ApiParametersBase)this.MemberwiseClone();

        clone.FullUrl = Utils.ConnectUrlParts(_baseApiUrl, parameters.Url ?? string.Empty);
        clone.Route = clone.FullUrl.Replace(Utils.ConnectUrlParts(PlaywrightConfig.BaseURL), "");
        clone.Method = parameters.Method;
        clone.ExpectedStatusCodes =
            parameters.ExpectedStatusCodes ?? PlaywrightConfig.ExpectedAPIResponseCodes;
        clone.Params = parameters;

        return clone;
    }
}
