using System.Reflection;
using Microsoft.Playwright;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Base;

public class Test : PlaywrightTestBase
{
    public Test(ITestOutputHelper outputHelper)
    {
        _state.Value = new TestContext();
        Output = outputHelper;
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
     * Task: Investigate and understand why do we need this handling of tracing groups.
     * Topics resolved with vibe-coding:
     * Handling parallel execution of ApiEndpointBase.WaitAsync() which has logic inside StepAsync().
     */
    public static async Task StepAsync(string name, Func<Task> action)
    {
        // Start the trace group without blocking the action.
        // This avoids races when multiple steps are started in parallel (Task.WhenAll)
        // and the action needs to register Playwright waiters immediately.
        var groupTask = Page.Context.Tracing.GroupAsync(name);
        Exception? actionException = null;

        try
        {
            await action();
        }
        catch (Exception ex)
        {
            actionException = ex;
            throw;
        }
        finally
        {
            try
            {
                await groupTask;
                await Page.Context.Tracing.GroupEndAsync();
            }
            catch when (actionException != null)
            {
                // Don't mask the original failure with tracing failures.
            }
        }
    }

    public static async Task<T> StepAsync<T>(string name, Func<Task<T>> action)
    {
        var groupTask = Page.Context.Tracing.GroupAsync(name);
        Exception? actionException = null;

        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            actionException = ex;
            throw;
        }
        finally
        {
            try
            {
                await groupTask;
                await Page.Context.Tracing.GroupEndAsync();
            }
            catch when (actionException != null) { }
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

    private static readonly AsyncLocal<TestContext> _state = new();

    private class TestContext
    {
        public ITestOutputHelper? Output;
        public IPage? Page;
        public IAPIRequestContext? Request;
    }
}
