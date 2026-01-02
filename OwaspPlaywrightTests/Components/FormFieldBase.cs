using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Components;

public class FormFieldBase(string componentName, ILocator? parent = null)
    : ComponentBase(
        componentName: componentName,
        body: parent == null
            ? Test.Page.Locator("mat-form-field")
            : parent.Locator("mat-form-field")
    )
{
    protected ILocator? _parent = parent;
}
