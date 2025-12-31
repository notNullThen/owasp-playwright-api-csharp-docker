using Microsoft.Playwright;
using OwaspPlaywrightTests.Support.Api.Types;

namespace OwaspPlaywrightTests.Support.Api;

public abstract class ApiEndpointBase(string baseApiUrl) : ApiParametersBase(baseApiUrl)
{
    protected int ActualStatusCode;

    public async Task<ApiResponse<T>> RequestAsync<T>(IAPIRequestContext context)
    {
        await ApiTestContext.Context.Tracing.GroupAsync(
            $"Request {Method} \"{Route}\", expect {string.Join(", ", ExpectedStatusCodes)}"
        );

        var targetContext = context ?? ApiTestContext.Context.APIRequest;

        var response = await targetContext.FetchAsync(
            FullUrl,
            new()
            {
                Method = Method.ToString(),
                Data = Params.Body?.ToString() ?? string.Empty,
                Timeout = PlaywrightConfig.ApiWaitTimeout,
            }
        );

        ActualStatusCode = response.Status;
        ValidateStatusCode();

        var result = await GetResponseAsync<T>(response);

        await ApiTestContext.Context.Tracing.GroupEndAsync();

        return result;
    }

    public async Task<BrowserApiResponse<T>> WaitAsync<T>(IPage page)
    {
        await ApiTestContext.Context.Tracing.GroupAsync(
            $"Wait for {Method} \"{Route}\" {string.Join(", ", ExpectedStatusCodes)}"
        );

        var response = await page.WaitForResponseAsync(
            (response) =>
            {
                // Ignore trailing slash and casing differences
                var actualUrl = Utils.NormalizeUrl(response.Url);
                var expectedUrl = Utils.NormalizeUrl(FullUrl);
                var requestMethod = response.Request.Method;

                if (!actualUrl.Contains(expectedUrl, StringComparison.InvariantCultureIgnoreCase))
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

        ActualStatusCode = response.Status;
        ValidateStatusCode();

        var result = await GetResponseAsync<T>(response);

        await ApiTestContext.Context.Tracing.GroupEndAsync();

        return result;
    }

    private static async Task<ApiResponse<T>> GetResponseAsync<T>(IAPIResponse response)
    {
        try
        {
            var responseBody = await response.JsonAsync<T>();
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
            var responseBody = await response.JsonAsync<T>();
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
                $"Expected to return {string.Join(", ", ExpectedStatusCodes)}, but got {ActualStatusCode}.\nEndpoint: {Method} {Route} "
            );
        }
    }
}
