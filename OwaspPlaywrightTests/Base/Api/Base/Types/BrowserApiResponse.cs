using Microsoft.Playwright;

namespace OwaspPlaywrightTests.Base.Api.Types;

public class BrowserApiResponse<T>
{
    public required IResponse Response;
    public T? ResponseBody;
}
