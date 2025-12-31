using Microsoft.Playwright;
using OwaspPlaywrightTests.Support.Api.Types;

namespace OwaspPlaywrightTests.Support.Api;

public record RequestActions<T>(Func<Task<T>> Request, Func<Task<T>> Wait);

public abstract class ApiBase : ApiEndpointBase
{
    protected readonly IAPIRequestContext Context;
    protected readonly IPage? Page;

    public ApiBase(string baseApiUrl, IAPIRequestContext context)
        : base(baseApiUrl)
    {
        Context = context;
    }

    public ApiBase(string baseApiUrl, IPage page)
        : base(baseApiUrl)
    {
        Context = page.APIRequest;
        Page = page;
    }
}
