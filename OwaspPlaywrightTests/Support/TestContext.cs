using System.Reflection;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Support;

public class TestContext : PlaywrightTestBase
{
    private static readonly AsyncLocal<ITestOutputHelper> _output = new();

    public static ITestOutputHelper Output
    {
        get => _output.Value!;
        set => _output.Value = value;
    }

    public TestContext(ITestOutputHelper output)
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
