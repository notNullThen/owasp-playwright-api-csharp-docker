using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Components;

public class Header() : ComponentBase("Page Header", TestContext.Page.Locator("app-navbar"))
{
    public AccountMenu AccountMenu => new();
    public SearchBar SearchBar => new();
}
