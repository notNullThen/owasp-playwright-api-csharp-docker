using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests.Components;

public class AccountMenu() : MenuBase("Account Menu", Test.Page.Locator("#navbarAccount"))
{
    public ILocator UserProfileItem => Menu.GetByRole(AriaRole.Menu).First;

    public async Task OpenAsync()
    {
        await Test.StepAsync(
            $"Open \"{ComponentName}\" menu",
            async () =>
            {
                var isVisible = await IsOpenAsync();
                if (!isVisible)
                {
                    await Body.ClickAsync();
                    await Utils.WaitForElementToBeStable(Menu);
                }

                // Here we use Playwright's Expect().ToBeVisibleAsync() instead of IsOpenAsync() for waiting until the menu is visible
                // and avoid potential timing issues.
                await Assertions.Expect(Menu).ToBeVisibleAsync();
            }
        );
    }
}
