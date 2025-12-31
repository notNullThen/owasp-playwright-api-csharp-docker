using System.Reflection;
using Microsoft.Playwright;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Support;

public class ApiTestContext
{
    private static readonly AsyncLocal<ITestOutputHelper> _xunitOutput = new();
    private static readonly AsyncLocal<IBrowserContext> _playwrightContext = new();

    public static ITestOutputHelper Output
    {
        get => _xunitOutput.Value!;
        set => _xunitOutput.Value = value;
    }

    public static IBrowserContext Context
    {
        get => _playwrightContext.Value!;
        set => _playwrightContext.Value = value;
    }

    public ApiTestContext(ITestOutputHelper output)
    {
        Output = output;
    }

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
