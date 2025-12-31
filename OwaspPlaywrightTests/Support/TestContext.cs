using System.Reflection;
using Microsoft.Playwright;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Support;

public class TestContext : PlaywrightTestBase
{
    private static readonly AsyncLocal<ITestOutputHelper> _xunitOutput = new();

    public static ITestOutputHelper Output
    {
        get => _xunitOutput.Value!;
        set => _xunitOutput.Value = value;
    }

    // public static new IPage? Page
    // {
    //     get => _playwrightPage.Value;
    //     set
    //     {
    //         _playwrightPage.Value = value!;
    //         Context = value!.Context!;
    //         Request = value.APIRequest;
    //     }
    // }

    public static new IPage? Page { get; private set; }

    public static void SetContext(IPage page)
    {
        Page = page;
        Context = page.Context;
        Request = page.APIRequest;
    }

    public static new IAPIRequestContext? Request { get; private set; }

    private static IBrowserContext? Context;

    public TestContext(ITestOutputHelper output)
    {
        Output = output;
    }

    public static async Task StartTracingGroupAsync(string name) =>
        await Context!.Tracing.GroupAsync(name);

    public static async Task EndTracingGroupAsync() => await Context!.Tracing.GroupEndAsync();

    public static string GetTestName()
    {
        if (Output != null)
        {
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
        }

        return $"UnknownTest_{Guid.NewGuid()}";
    }
}
