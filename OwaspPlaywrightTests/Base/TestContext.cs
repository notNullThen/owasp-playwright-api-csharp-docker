using System.Reflection;
using Microsoft.Playwright;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Base;

public class TestContext : PlaywrightTestBase
{
    public static ITestOutputHelper? Output;

    public static new IPage? Page { get; private set; }

    public static void SetContext(IPage page)
    {
        Page = page;
        Context = page.Context;
        Request = page.APIRequest;
    }

    public static new IAPIRequestContext? Request;

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
