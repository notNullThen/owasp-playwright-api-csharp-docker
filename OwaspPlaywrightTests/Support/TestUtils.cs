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
