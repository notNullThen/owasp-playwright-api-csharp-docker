using Microsoft.Playwright;

namespace OwaspPlaywrightTests.Base.Api.Types;

public class ApiResponse<T>
{
    public required IAPIResponse Response;
    public T? ResponseBody;
}
