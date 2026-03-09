using OwaspPlaywrightTests.Base.ApiHandler.Types;

namespace OwaspPlaywrightTests.Base.ApiHandler;

public abstract class ApiEndpointBase(string apiBaseUrl)
{
    public ApiAction<T> Action<T>(RequestParameters parameters) => new(apiBaseUrl, parameters);
}
