using Microsoft.Playwright;
using OwaspPlaywrightTests.Base.ApiHandler.Types;

namespace OwaspPlaywrightTests.Base.ApiHandler;

public class ApiAction<T>
{
    private readonly ApiBase _apiBase;

    public ApiAction(ApiBase apiBase, RequestParameters? parameters = null)
    {
        _apiBase = apiBase;
        if (parameters != null)
            apiBase.SetParameters(parameters);
    }

    public IAPIRequestContext Context => _apiBase.Context;
    public IPage? Page => _apiBase.Page;

    public async Task<ApiResponse<T>> RequestAsync()
    {
        return await _apiBase.RequestAsync<T>(Context);
    }

    public async Task<BrowserApiResponse<T>> WaitAsync()
    {
        if (Page == null)
        {
            throw new PlaywrightException(
                $"You can use {nameof(_apiBase.WaitAsync)}() only in the context of UI Tests (The '{nameof(IPage)}' should be available)."
            );
        }

        return await _apiBase.WaitAsync<T>(Page);
    }
}
