using Microsoft.Playwright;

namespace OwaspPlaywrightTests.Base.Api.Base.Types;

public class ApiResponse<T>
{
    public required IAPIResponse Response;
    public T ResponseBody;
}
