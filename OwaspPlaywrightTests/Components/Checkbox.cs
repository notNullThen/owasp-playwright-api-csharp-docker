using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Components;

// Didn't use constructor overloading as it is the only way to implement the GetByName() method properly.
public class Checkbox(string componentName, ILocator? parent)
    : ComponentBase(
        componentName.EndsWith(" Checkbox", StringComparison.InvariantCultureIgnoreCase)
            ? componentName
            : componentName + " Checkbox",
        parent == null ? Test.Page.Locator("mat-checkbox") : parent.Locator("mat-checkbox")
    )
{
    private const string CheckedClass = "mdc-checkbox--selected";
    private readonly ILocator? _parent = parent;

    public ILocator CheckboxInput => Body.GetByRole(AriaRole.Checkbox);

    public async Task CheckAsync()
    {
        await Test.StepAsync(
            $"Check \"{_componentName}\" checkbox",
            async () =>
            {
                var isChecked = await IsCheckedAsync();
                if (!isChecked)
                {
                    await CheckboxInput.ClickAsync();
                }

                await ShouldBeCheckedAsync();
            }
        );
    }

    public async Task UncheckAsync()
    {
        await Test.StepAsync(
            $"Uncheck \"{_componentName}\" checkbox",
            async () =>
            {
                var isChecked = await IsCheckedAsync();
                if (isChecked)
                {
                    await CheckboxInput.ClickAsync();
                }

                await ShouldBeUncheckedAsync();
            }
        );
    }

    // In this case it would be better to use .isChecked() from Playwright directly,
    // but sometimes custom checkboxes do not reflect the state properly in the accessible tree.
    // So for demonstration purposes, we implement our own isChecked() method.
    public async Task<bool> IsCheckedAsync()
    {
        var classAttribute = await CheckboxInput.GetAttributeAsync("class");
        return classAttribute!.Contains(CheckedClass);
    }

    public async Task ShouldBeCheckedAsync()
    {
        await Test.StepAsync(
            $"Verify \"{_componentName}\" checkbox is checked",
            async () =>
            {
                var isChecked = await IsCheckedAsync();
                Assert.True(isChecked, $"{_componentName} is not checked, but should be.");
            }
        );
    }

    public async Task ShouldBeUncheckedAsync()
    {
        await Test.StepAsync(
            $"Verify \"{_componentName}\" checkbox is not checked",
            async () =>
            {
                var isChecked = await IsCheckedAsync();
                Assert.False(isChecked, $"{_componentName} is checked, but should be unchecked.");
            }
        );
    }

    public Checkbox GetByName(string name)
    {
        var checkbox = new Checkbox(_componentName, _parent);
        checkbox.Body = checkbox.Body.Filter(
            new() { Has = Page.GetByText(name, new() { Exact = true }) }
        );
        return checkbox;
    }
}
