using Microsoft.Playwright;
using OwaspPlaywrightTests.Base.Api.Types;

namespace OwaspPlaywrightTests.Base.Api.Base;

public class ApiAction<T>(RequestParameters parameters, ApiBase apiBase)
{
    public readonly IAPIRequestContext Context = apiBase.Context;
    public readonly IPage? Page = apiBase.Page;

    public async Task<ApiResponse<T>> Request()
    {
        apiBase.SetParameters(parameters);
        return await apiBase.RequestAsync<T>(Context);
    }

    public async Task<BrowserApiResponse<T>> Wait()
    {
        if (Page == null)
        {
            throw new PlaywrightException(
                $"You can use {nameof(apiBase.WaitAsync)}() only in the context of UI Tests (The '{nameof(IPage)}' should be available)."
            );
        }

        apiBase.SetParameters(parameters);
        return await apiBase.WaitAsync<T>(Page);
    }
}
