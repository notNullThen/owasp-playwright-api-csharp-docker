using Microsoft.Playwright;

namespace OwaspPlaywrightTests.Support.Api.Types;

public class BrowserApiResponse<T>
{
    public required IResponse Response;
    public T? ResponseBody;
}
