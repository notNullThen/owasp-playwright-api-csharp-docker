using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests;

public class UnitTest1 : PlaywrightDriver
{
    [Fact]
    public async Task Test1()
    {
        await Page.GotoAsync("/");
        await Page.WaitForTimeoutAsync(1000);
        await Expect(Page).ToHaveURLAsync("https://example.com/");
    }
}
