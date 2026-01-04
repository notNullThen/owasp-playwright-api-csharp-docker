using Microsoft.Playwright;
using OwaspPlaywrightTests.Base.ApiHandler.Types;

namespace OwaspPlaywrightTests.Base.ApiHandler;

public class ApiEndpointBase : ApiBase
{
    public readonly IAPIRequestContext Context;
    public readonly IPage? Page;

    public ApiEndpointBase(string baseApiUrl)
        : base(baseApiUrl)
    {
        if (Test.Request == null && Test.Page == null)
        {
            throw new PlaywrightException(
                $"You need to provide at least '{nameof(Context)}' or '{nameof(Page)}' parameters to create an instance of '{nameof(ApiBase)}'."
            );
        }

        Context = Test.Request!;
        Page = Test.Page;

        Context = (Page?.APIRequest ?? Context)!;
    }

    public ApiAction<T> Action<T>(RequestParameters parameters) =>
        new(apiBase: this, parameters: parameters);

    public ApiAction<dynamic> Action(RequestParameters parameters) =>
        new(apiBase: this, parameters: parameters);

    public void SetParameters(RequestParameters parameters)
    {
        AquireParameters(parameters);
    }
}
