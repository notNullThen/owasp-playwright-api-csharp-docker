using Microsoft.Playwright;

namespace OwaspPlaywrightTests.Support.Api.Types;

public class ApiResponse<T>
{
    public required IAPIResponse Response;
    public T? ResponseBody;
}
