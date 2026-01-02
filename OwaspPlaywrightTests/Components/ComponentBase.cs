using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Components;

public abstract class ComponentBase(string componentName, ILocator body)
{
    public IPage Page => Test.Page;
    public ILocator Body = body;
    protected string _componentName = componentName;

    public async Task<int> CountAsync()
    {
        return await Body.CountAsync();
    }
}
