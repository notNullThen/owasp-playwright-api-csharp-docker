using Microsoft.Playwright;

namespace OwaspPlaywrightTests.Base.ApiHandler.Types;

public class BrowserApiResponse<T>
{
    public required IResponse Response;
    public T? ResponseBody;
}
