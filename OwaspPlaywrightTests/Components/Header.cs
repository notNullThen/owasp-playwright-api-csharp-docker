using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Components;

public class Header()
    : ComponentBase(componentName: "Page Header", body: Test.Page.Locator("app-navbar"))
{
    public AccountMenu AccountMenu => new();
    public SearchBar SearchBar => new();
}
