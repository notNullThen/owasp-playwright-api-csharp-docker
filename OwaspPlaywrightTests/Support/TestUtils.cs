using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Support;

public static class TestUtils
{
    public const string PRICE_SYMBOL = "¤";

    public static async Task WaitForElementToBeStableAsync(ILocator element)
    {
        var handle = await element.ElementHandleAsync();
        await handle.WaitForElementStateAsync(ElementState.Stable);
    }

    public static string FormatPrice(decimal price)
    {
        string priceString = price <= 99.99m ? price.ToString("F2") : price.ToString("F0");
        return $"{priceString}{PRICE_SYMBOL}";
    }

    public static float GetPriceFromText(string priceText)
    {
        return float.Parse(priceText.Replace(PRICE_SYMBOL, ""));
    }

    public static async Task DismissCookiesAsync()
    {
        await Test.Page.Context.AddCookiesAsync([
            new Cookie
            {
                Name = "cookieconsent_status",
                Value = "dismiss",
                Url = PlaywrightConfig.BaseURL,
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
                Url = PlaywrightConfig.BaseURL,
            },
        ]);
    }

    public static string ConnectUrlParts(params string[] parts)
    {
        var connectedParts = string.Join(
            "/",
            parts
                .Where(part => !string.IsNullOrEmpty(part))
                .Select(NormalizeUrl)
                .Where(part => part.Trim().Length > 0)
        );

        return connectedParts + "/";
    }

    public static string NormalizeUrl(string url)
    {
        return RemoveLeadingSlash(RemoveTrailingSlash(url));
    }

    public static string RemoveTrailingSlash(string url)
    {
        return url.EndsWith('/') ? url.Substring(0, url.Length - 1) : url;
    }

    public static string RemoveLeadingSlash(string url)
    {
        return url.StartsWith('/') ? url.Substring(1) : url;
    }
}
