using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Components;

// Named this class as "HeaderComponent" to avoid confusion with Playwright's "Header" class
public class HeaderComponent()
    : ComponentBase(componentName: "Page Header", body: Test.Page.Locator("app-navbar"))
{
    public AccountMenu AccountMenu => new();
    public SearchBar SearchBar => new();
}
