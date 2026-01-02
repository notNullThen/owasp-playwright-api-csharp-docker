using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests.Components;

public class Dropdown : FormFieldBase
{
    public Dropdown(string componentName, ILocator? parent = null)
        : base(
            componentName: componentName.EndsWith(" Dropdown")
                ? componentName
                : componentName + " Dropdown",
            parent: parent
        )
    {
        Body =
            parent == null
                ? Test.Page.GetByRole(AriaRole.Combobox)
                : parent.GetByRole(AriaRole.Combobox);
    }

    public ILocator Input => Body.GetByRole(AriaRole.Combobox);

    public ILocator Options => Page.GetByRole(AriaRole.Listbox);

    public Dropdown GetByName(string name)
    {
        var dropdown = new Dropdown(componentName: _componentName, parent: _parent);
        dropdown.Body = dropdown.Body.Filter(
            new() { Has = Page.GetByRole(AriaRole.Combobox, new() { Name = name }) }
        );
        return dropdown;
    }

    public ILocator GetOptionByName(string name)
    {
        return Options.GetByRole(AriaRole.Option, new() { Name = name });
    }

    public async Task SelectAsync(string name)
    {
        await Test.StepAsync(
            $"Select \"{_componentName}\" dropdown \"{name}\" option",
            async () =>
            {
                await Input.ClickAsync();
                await Utils.WaitForElementToBeStableAsync(Options);

                await GetOptionByName(name).ClickAsync();
                await Assertions.Expect(Input).ToHaveTextAsync(name);
            }
        );
    }
}
