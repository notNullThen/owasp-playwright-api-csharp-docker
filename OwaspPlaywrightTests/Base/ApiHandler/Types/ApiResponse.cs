using Microsoft.Playwright;

namespace OwaspPlaywrightTests.Base.ApiHandler.Types;

public class ApiResponse<T>
{
    public required IAPIResponse Response;
    public T? ResponseBody;
}
