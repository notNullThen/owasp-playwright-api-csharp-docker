using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Components;

namespace OwaspPlaywrightTests.Pages;

public abstract class PageBase(string url)
{
    public IPage Page => Test.Page;
    public HeaderComponent Header => new();

    public ILocator HeaderText => Page.Locator(".heading");

    public async Task GoToAsync()
    {
        await Test.StepAsync(
            $"Go to '{url}' page",
            async () =>
            {
                await Page.GotoAsync(url);
            }
        );
    }
}
