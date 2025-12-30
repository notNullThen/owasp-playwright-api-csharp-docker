using OwaspPlaywrightTests.Support;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests;

public class UnitTest1(ITestOutputHelper output) : PlaywrightDriver(output)
{
    [Fact]
    public async Task Test1()
    {
        await Page.GotoAsync("/");
    }
}
