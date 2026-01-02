using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Components;

public class InputFormField(string componentName, ILocator? parent)
    : FormFieldBase(
        componentName.EndsWith(" Input Field", StringComparison.InvariantCultureIgnoreCase)
            ? componentName
            : componentName + " Input Field",
        parent
    )
{
    private const string ErrorClass = "mat-form-field-invalid";

    public ILocator Input => Body.GetByRole(AriaRole.Textbox);

    public InputFormField GetByLocator(string locator)
    {
        var inputFormField = new InputFormField(_componentName, parent);
        inputFormField.Body = inputFormField.Body.Filter(new() { Has = Page.Locator(locator) });
        return inputFormField;
    }

    public InputFormField GetByAreaLabel(string areaLabel)
    {
        var inputFormField = new InputFormField(_componentName, parent);
        inputFormField.Body = inputFormField.Body.Filter(
            new() { Has = Page.GetByRole(AriaRole.Textbox, new() { Name = areaLabel }) }
        );
        return inputFormField;
    }

    public async Task FillAsync(string value)
    {
        await Test.StepAsync(
            $"Fill \"{_componentName}\" input field with value: \"{value}\"",
            async () => await Input.FillAsync(value)
        );
    }

    public async Task ShouldHaveErrorAsync()
    {
        await Test.StepAsync(
            $"Verify \"{_componentName}\" input field has an error",
            async () =>
                Assert.True(
                    await HasErrorAsync(),
                    $"Expected \"{_componentName}\" to have an error, but it does not."
                )
        );
    }

    public async Task ShouldNotHaveErrorAsync()
    {
        await Test.StepAsync(
            $"Verify \"{_componentName}\" input field has no error",
            async () =>
                Assert.False(
                    await HasErrorAsync(),
                    $"Expected \"{_componentName}\" not to have an error, but it does."
                )
        );
    }

    public async Task<bool> HasErrorAsync()
    {
        var classAttribute = await Body.GetAttributeAsync("class");
        return classAttribute!.Contains(ErrorClass);
    }

    public async Task PressEnterAsync()
    {
        await Test.StepAsync(
            $"Press Enter key on \"{_componentName}\" input field",
            async () => await Input.PressAsync("Enter")
        );
    }
}
