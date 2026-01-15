using System.Text.Json;
using Microsoft.Playwright;
using OwaspPlaywrightTests.Base.ApiHandler.Types;

namespace OwaspPlaywrightTests.Base.ApiHandler;

public abstract class ApiBase(string baseApiUrl) : ApiParametersBase(baseApiUrl)
{
    protected int _actualStatusCode;

    private string? _errorMessage;

    public async Task<ApiResponse<T>> RequestAsync<T>(IAPIRequestContext context)
    {
        var response = await RequestBaseAsync(context);
        return await GetResponseAsync<T>(response);
    }

    public async Task<ApiResponse<dynamic>> RequestAsync(IAPIRequestContext context)
    {
        var response = await RequestBaseAsync(context);
        return await GetResponseAsync(response);
    }

    private async Task<IAPIResponse> RequestBaseAsync(IAPIRequestContext context)
    {
        return await Test.StepAsync(
            $"Request {_method} \"{_route}\", expect {string.Join(", ", _expectedStatusCodes)}",
            async () =>
            {
                // Separate bodyJson variable for better debug
                var bodyJson = JsonSerializer.Serialize(_body);
                var response = await context.FetchAsync(
                    _fullUrl,
                    new()
                    {
                        Method = _method.ToString(),
                        Data = bodyJson,
                        Headers =
                        [
                            new("Content-Type", "application/json"),
                            new("Authorization", GetToken() ?? string.Empty),
                        ],
                        Timeout = s_apiWaitTimeout,
                    }
                );

                _actualStatusCode = response.Status;
                ValidateStatusCode();

                return response;
            }
        );
    }

    public async Task<BrowserApiResponse<T>> WaitAsync<T>(IPage page)
    {
        var response = await WaitBaseAsync<T>(page);
        return await GetResponseAsync<T>(response);
    }

    public async Task<BrowserApiResponse<dynamic>> WaitAsync(IPage page)
    {
        var response = await WaitBaseAsync<dynamic>(page);
        return await GetResponseAsync(response);
    }

    private async Task<IResponse> WaitBaseAsync<T>(IPage page)
    {
        return await Test.StepAsync(
            $"Wait for {_method} \"{_route}\" {string.Join(", ", _expectedStatusCodes)}",
            async () =>
            {
                var response = await page.WaitForResponseAsync(
                    (response) =>
                    {
                        // Ignore trailing slash and casing differences
                        var actualUrl = NormalizeUrl(response.Url);
                        var expectedUrl = NormalizeUrl(_fullUrl);
                        var requestMethod = response.Request.Method;

                        if (
                            !actualUrl.Contains(
                                expectedUrl,
                                StringComparison.InvariantCultureIgnoreCase
                            )
                        )
                        {
                            return false;
                        }

                        if (
                            !requestMethod.Equals(
                                _method.ToString(),
                                StringComparison.InvariantCultureIgnoreCase
                            )
                        )
                        {
                            return false;
                        }

                        return true;
                    },
                    new() { Timeout = s_apiWaitTimeout }
                );

                _errorMessage = response.StatusText;

                _actualStatusCode = response.Status;
                ValidateStatusCode();

                return response;
            }
        );
    }

    private static async Task<ApiResponse<T>> GetResponseAsync<T>(IAPIResponse response)
    {
        // For better debugging convenience created JSON string with TextAsync()
        // and then used JsonSerializer.Deserialize<T>() instead of Playwright's JsonAsync<T>()
        var responseString = await response.TextAsync();
        var responseBody = JsonSerializer.Deserialize<T>(responseString)!;
        return new() { Response = response, ResponseBody = responseBody };
    }

    private static async Task<ApiResponse<dynamic>> GetResponseAsync(IAPIResponse response)
    {
        return new() { Response = response, ResponseBody = default };
    }

    private static async Task<BrowserApiResponse<T>> GetResponseAsync<T>(IResponse response)
    {
        var responseString = await response.TextAsync();
        var responseBody = JsonSerializer.Deserialize<T>(responseString)!;
        return new() { Response = response, ResponseBody = responseBody };
    }

    private static async Task<BrowserApiResponse<dynamic>> GetResponseAsync(IResponse response)
    {
        return new() { Response = response, ResponseBody = default };
    }

    private void ValidateStatusCode()
    {
        if (!_expectedStatusCodes.Contains(_actualStatusCode))
        {
            throw new Exception(
                $"Expected to return {string.Join(", ", _expectedStatusCodes)}, but got {_actualStatusCode}.\nEndpoint: {_method} {_route}\nError Message: {_errorMessage}"
            );
        }
    }
}
