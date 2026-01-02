using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Components;

public class FormFieldBase(string componentName, ILocator? parent)
    : ComponentBase(
        componentName,
        parent == null ? Test.Page.Locator("mat-form-field") : parent.Locator("mat-form-field")
    ) { }
