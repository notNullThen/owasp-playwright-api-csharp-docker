using System.Reflection;
using Microsoft.Playwright;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Base;

public class Test : PlaywrightTestBase
{
    public Test(ITestOutputHelper output)
    {
        _state.Value = new TestContext();
        Output = output;
    }

    public static ITestOutputHelper? Output
    {
        get => _state.Value?.Output;
        set { _state.Value?.Output = value; }
    }

    public static new IPage Page
    {
        get => _state.Value!.Page!;
        set
        {
            if (value == null)
                throw new PlaywrightException("The Playwright Page cannot be null.");

            _state.Value!.Page = value;
            Request = value.APIRequest;
        }
    }

    public static new IAPIRequestContext? Request
    {
        get => _state.Value?.Request;
        set { _state.Value?.Request = value; }
    }

    public static async Task StepAsync(string name, Func<Task> action)
    {
        await Page.Context!.Tracing.GroupAsync(name);
        try
        {
            await action();
        }
        finally
        {
            await Page.Context!.Tracing.GroupEndAsync();
        }
    }

    public static async Task<T> StepAsync<T>(string name, Func<Task<T>> action)
    {
        await Page.Context!.Tracing.GroupAsync(name);
        try
        {
            return await action();
        }
        finally
        {
            await Page.Context!.Tracing.GroupEndAsync();
        }
    }

    public static string GetTestName()
    {
        if (Output == null)
            throw new PlaywrightException("The XUnit Output cannot be null.");

        var type = Output!.GetType();
        var testMember = type.GetField("test", BindingFlags.Instance | BindingFlags.NonPublic);

        if (testMember != null)
        {
            var test = (ITest?)testMember.GetValue(Output);
            if (test != null)
            {
                return string.Join(" - ", test.DisplayName.Split('.').Skip(1));
            }
        }

        return $"UnknownTest_{Guid.NewGuid()}";
    }

    private static readonly AsyncLocal<TestContext> _state = new();

    private class TestContext
    {
        public ITestOutputHelper? Output;
        public IPage? Page;
        public IAPIRequestContext? Request;
        public IBrowserContext? Context;
    }
}
