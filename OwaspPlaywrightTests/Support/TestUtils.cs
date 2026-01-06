using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Support;

public static class TestUtils
{
    public static async Task WaitForElementToBeStableAsync(ILocator element)
    {
        var handle = await element.ElementHandleAsync();
        await handle.WaitForElementStateAsync(ElementState.Stable);
    }

    // Waits for the base URL to be ready before all tests run
    // Resolves Docker container startup lag
    public static async Task WaitForBaseUrlReadyAsync()
    {
        var httpClient = new HttpClient();
        var maxAttempts = TestConfig.BaseUrlReadyRetries;
        var delayBetweenAttemptsMs = TestConfig.BaseUrlReadyDelayMs;
        var baseUrl = TestConfig.BaseUrl;
        var attempt = 0;
        var isReady = false;

        while (attempt < maxAttempts && !isReady)
        {
            try
            {
                using var response = await httpClient.GetAsync(baseUrl);
                if (response.IsSuccessStatusCode)
                {
                    isReady = true;
                    break;
                }
            }
            catch
            {
                // Ignore errors and retry
            }

            attempt++;
            await Task.Delay(delayBetweenAttemptsMs);
        }

        if (!isReady)
        {
            throw new InvalidOperationException(
                $"Base URL {baseUrl} does not respond after {maxAttempts} attempts."
            );
        }
    }

    public static string FormatPrice(decimal price)
    {
        string priceString = price <= 99.99m ? price.ToString("F2") : price.ToString("F0");
        return $"{priceString}{TestConfig.PriceSymbol}";
    }

    public static float GetPriceFromText(string priceText)
    {
        return float.Parse(priceText.Replace(TestConfig.PriceSymbol, ""));
    }

    public static async Task DismissCookiesAsync()
    {
        await Test.Page.Context.AddCookiesAsync([
            new Cookie
            {
                Name = "cookieconsent_status",
                Value = "dismiss",
                Url = TestConfig.BaseUrl,
            },
        ]);
    }

    public static async Task DismissWelcomeBannerAsync()
    {
        await Test.Page.Context.AddCookiesAsync([
            new Cookie
            {
                Name = "welcomebanner_status",
                Value = "dismiss",
                Url = TestConfig.BaseUrl,
            },
        ]);
    }
}
