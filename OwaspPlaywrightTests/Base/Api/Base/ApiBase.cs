using Microsoft.Playwright;
using OwaspPlaywrightTests.Base.Api.Types;

namespace OwaspPlaywrightTests.Base.Api.Base;

public class ApiBase : ApiEndpointBase
{
    public readonly IAPIRequestContext Context;
    public readonly IPage? Page;

    public ApiBase(string baseApiUrl)
        : base(baseApiUrl)
    {
        if (TestContext.Request == null && TestContext.Page == null)
        {
            throw new PlaywrightException(
                $"You need to provide at least '{nameof(Context)}' or '{nameof(Page)}' parameters to create an instance of '{nameof(ApiBase)}'."
            );
        }

        Context = TestContext.Request!;
        Page = TestContext.Page;

        Context = (Page?.APIRequest ?? Context)!;
    }

    public ApiAction<T> Action<T>(RequestParameters parameters) => new(parameters, this);

    public void SetParameters(RequestParameters parameters)
    {
        AquireParameters(parameters);
    }
}
