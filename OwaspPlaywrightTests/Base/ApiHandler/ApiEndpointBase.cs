using System.Text.Json;
using Microsoft.Playwright;
using OwaspPlaywrightTests.Base.ApiHandler.Types;
using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests.Base.ApiHandler;

public abstract class ApiEndpointBase(string baseApiUrl) : ApiParametersBase(baseApiUrl)
{
    protected int ActualStatusCode;

    private string? ErrorMessage;

    public async Task<ApiResponse<T>> RequestAsync<T>(IAPIRequestContext context)
    {
        return await Test.StepAsync(
            $"Request {Method} \"{Route}\", expect {string.Join(", ", ExpectedStatusCodes)}",
            async () =>
            {
                // Separate bodyJson variable for better debug
                var bodyJson = JsonSerializer.Serialize(Body);
                var response = await context.FetchAsync(
                    FullUrl,
                    new()
                    {
                        Method = Method.ToString(),
                        Data = bodyJson,
                        Headers = [new("Content-Type", "application/json")],
                        Timeout = PlaywrightConfig.ApiWaitTimeout,
                    }
                );

                ActualStatusCode = response.Status;
                ValidateStatusCode();

                return await GetResponseAsync<T>(response);
            }
        );
    }

    public async Task<BrowserApiResponse<T>> WaitAsync<T>(IPage page)
    {
        return await Test.StepAsync(
            $"Wait for {Method} \"{Route}\" {string.Join(", ", ExpectedStatusCodes)}",
            async () =>
            {
                var response = await page.WaitForResponseAsync(
                    (response) =>
                    {
                        // Ignore trailing slash and casing differences
                        var actualUrl = Utils.NormalizeUrl(response.Url);
                        var expectedUrl = Utils.NormalizeUrl(FullUrl);
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
                                Method.ToString(),
                                StringComparison.InvariantCultureIgnoreCase
                            )
                        )
                        {
                            return false;
                        }

                        return true;
                    },
                    new() { Timeout = ApiWaitTimeout }
                );

                ErrorMessage = response.StatusText;

                ActualStatusCode = response.Status;
                ValidateStatusCode();

                return await GetResponseAsync<T>(response);
            }
        );
    }

    private static async Task<ApiResponse<T>> GetResponseAsync<T>(IAPIResponse response)
    {
        try
        {
            // For better debugging convenience created JSON string with TextAsync()
            // and then used JsonSerializer.Deserialize<T>() instead of Playwright's JsonAsync<T>()
            var responseString = await response.TextAsync();
            var responseBody = JsonSerializer.Deserialize<T>(responseString)!;
            return new() { Response = response, ResponseBody = responseBody };
        }
        catch
        {
            return new() { Response = response, ResponseBody = default };
        }
    }

    private static async Task<BrowserApiResponse<T>> GetResponseAsync<T>(IResponse response)
    {
        try
        {
            var responseString = await response.TextAsync();
            var responseBody = JsonSerializer.Deserialize<T>(responseString)!;
            return new() { Response = response, ResponseBody = responseBody };
        }
        catch
        {
            return new() { Response = response, ResponseBody = default };
        }
    }

    private void ValidateStatusCode()
    {
        if (!ExpectedStatusCodes.Contains(ActualStatusCode))
        {
            throw new Exception(
                $"Expected to return {string.Join(", ", ExpectedStatusCodes)}, but got {ActualStatusCode}.\nEndpoint: {Method} {Route}\nError Message: {ErrorMessage}"
            );
        }
    }
}
