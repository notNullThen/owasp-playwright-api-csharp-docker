using Microsoft.Playwright;

namespace OwaspPlaywrightTests.Base.Api.Base.Types;

public class BrowserApiResponse<T>
{
    public required IResponse Response;
    public T? ResponseBody;
}
