using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Pages;

public abstract class PageBase(string url)
{
    public IPage Page => Test.Page;

    public async Task GoToAsync()
    {
        await Page.GotoAsync(url);
    }
}
