using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests.Components;

public class AccountMenu()
    : MenuBase(componentName: "Account Menu", body: Test.Page.Locator("#navbarAccount"))
{
    public ILocator UserProfileItem => Menu.GetByRole(AriaRole.Menuitem).First;

    public async Task OpenAsync()
    {
        await Test.StepAsync(
            $"Open \"{_componentName}\" menu",
            async () =>
            {
                var isVisible = await IsOpenAsync();
                if (!isVisible)
                {
                    await Body.ClickAsync();
                    await Utils.WaitForElementToBeStableAsync(Menu);
                }

                // Here we use Playwright's Expect().ToBeVisibleAsync() instead of IsOpenAsync() for waiting until the menu is visible
                // and avoid potential timing issues.
                await Assertions.Expect(Menu).ToBeVisibleAsync();
            }
        );
    }
}
