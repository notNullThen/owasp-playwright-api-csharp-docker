namespace OwaspPlaywrightTests.Components;

using Microsoft.Playwright;

public abstract class IterableComponentBase(string componentName, ILocator body)
    : ComponentBase(componentName, body)
{
    private readonly ILocator _body = body;

    public abstract IterableComponentBase GetByText(string text);
    public abstract IterableComponentBase GetByIndex(int index);

    public async Task<int> CountAsync() => await Body.CountAsync();

    protected T GetByTextBase<T>(string text)
        where T : IterableComponentBase
    {
        var component = (T)Create(_body);
        component.Body = Body.Filter(new() { HasText = text });
        return component;
    }

    protected T GetByIndexBase<T>(int index)
        where T : IterableComponentBase
    {
        var component = (T)Create(_body);
        component.Body = Body.Nth(index);
        return component;
    }

    protected abstract IterableComponentBase Create(ILocator body);
}
