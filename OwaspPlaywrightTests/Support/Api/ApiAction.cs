using Microsoft.Playwright;
using OwaspPlaywrightTests.Support.Api.Types;

namespace OwaspPlaywrightTests.Support.Api;

public class Action<T> : ApiBase
{
    public Action(string baseApiUrl, IAPIRequestContext context, RequestParameters parameters)
        : base(baseApiUrl, context)
    {
        AquireParameters(parameters);
    }

    public Action(string baseApiUrl, IPage page, RequestParameters parameters)
        : base(baseApiUrl, page)
    {
        AquireParameters(parameters);
    }

    public async Task<ApiResponse<T>> RequestAsync()
    {
        return await RequestAsync<T>(Context);
    }

    public async Task<BrowserApiResponse<T>> WaitAsync()
    {
        if (Page == null)
        {
            throw new PlaywrightException(
                $"You can use {nameof(WaitAsync)}() only in the context of UI Tests (context should be of '{nameof(IPage)}' type)"
            );
        }

        return await WaitAsync<T>(Page);
    }
}
