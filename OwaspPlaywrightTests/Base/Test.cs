using System.Reflection;
using Microsoft.Playwright;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Base;

public class Test : PlaywrightTestBase
{
    private static readonly AsyncLocal<TestContext> _state = new();
    private static readonly AsyncLocal<int> _logicalDepth = new();

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

    /**
     * TODO: StepAsync() functions are VIBE-CODED AREA.
     * Task: Investigate and understand if it is possible to simplify this further.
     * Topics resolved with vibe-coding:
     * Handling parallel execution of ApiEndpointBase.WaitAsync() which has logic inside StepAsync().
     */
    public static async Task StepAsync(string name, Func<Task> action)
    {
        EnsureContext();

        _logicalDepth.Value++;
        Interlocked.Increment(ref _state.Value!.RunningSteps);

        // Only create a tracing group if we are running sequentially.
        // If RunningSteps > _logicalDepth.Value, it implies parallel execution (Task.WhenAll),
        // where Playwright's stack-based tracing groups would conflict.
        bool isSequential = _state.Value.RunningSteps == _logicalDepth.Value;

        if (isSequential)
        {
            await Page.Context.Tracing.GroupAsync(name);
        }

        try
        {
            await action();
        }
        finally
        {
            if (isSequential)
            {
                await Page.Context.Tracing.GroupEndAsync();
            }
            Interlocked.Decrement(ref _state.Value!.RunningSteps);
            _logicalDepth.Value--;
        }
    }

    public static async Task<T> StepAsync<T>(string name, Func<Task<T>> action)
    {
        EnsureContext();

        _logicalDepth.Value++;
        Interlocked.Increment(ref _state.Value!.RunningSteps);

        bool isSequential = _state.Value.RunningSteps == _logicalDepth.Value;

        if (isSequential)
        {
            await Page.Context.Tracing.GroupAsync(name);
        }

        try
        {
            return await action();
        }
        finally
        {
            if (isSequential)
            {
                await Page.Context.Tracing.GroupEndAsync();
            }
            Interlocked.Decrement(ref _state.Value!.RunningSteps);
            _logicalDepth.Value--;
        }
    }

    public static string GetTestName()
    {
        if (Output == null)
            throw new PlaywrightException("The XUnit Output cannot be null.");

        var type = Output.GetType();
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

    private static void EnsureContext()
    {
        _state.Value ??= new TestContext();
    }

    private class TestContext
    {
        public ITestOutputHelper? Output;
        public IPage? Page;
        public IAPIRequestContext? Request;
        public int RunningSteps;
    }
}
