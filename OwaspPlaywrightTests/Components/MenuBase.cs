using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Components;

public class MenuBase(string componentName, ILocator body) : ComponentBase(componentName, body)
{
    public ILocator Menu => Page.GetByRole(AriaRole.Menu);

    public async Task CloseAsync()
    {
        await Test.StepAsync(
            $"Close \"{_componentName}\" menu",
            async () =>
            {
                var oldMenuCount = await Menu.CountAsync();
                await Page.Keyboard.PressAsync("Escape");
                await Assertions.Expect(Menu).ToHaveCountAsync(oldMenuCount - 1);
            }
        );
    }

    public async Task<bool> IsOpenAsync()
    {
        return await Menu.IsVisibleAsync();
    }
}
