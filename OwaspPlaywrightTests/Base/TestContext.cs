using System.Reflection;
using Microsoft.Playwright;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Base;

public class TestContext : PlaywrightTestBase
{
    public TestContext(ITestOutputHelper output)
    {
        _state.Value = new TestContextState();
        Output = output;
    }

    public static ITestOutputHelper? Output
    {
        get => _state.Value?.Output;
        set { _state.Value?.Output = value; }
    }

    public static new IPage? Page
    {
        get => _state.Value?.Page;
        set
        {
            if (value == null)
                throw new PlaywrightException("The Playwright Page cannot be null.");

            _state.Value?.Page = value;
            _context = value.Context;
            Request = value.APIRequest;
        }
    }

    public static new IAPIRequestContext? Request
    {
        get => _state.Value?.Request;
        set { _state.Value?.Request = value; }
    }

    public static async Task StartTracingGroupAsync(string name) =>
        await _context!.Tracing.GroupAsync(name);

    public static async Task EndTracingGroupAsync() => await _context!.Tracing.GroupEndAsync();

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

    private static readonly AsyncLocal<TestContextState> _state = new();

    private static IBrowserContext? _context
    {
        get => _state.Value?.Context;
        set { _state.Value?.Context = value; }
    }

    private class TestContextState
    {
        public ITestOutputHelper? Output;
        public IPage? Page;
        public IAPIRequestContext? Request;
        public IBrowserContext? Context;
    }
}
