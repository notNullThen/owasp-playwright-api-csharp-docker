using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Support.Helpers;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests;

public class UnitTest1(ITestOutputHelper output) : TestContext(output)
{
    [Fact]
    public async Task Test1()
    {
        var usersHelper = new UsersHelper();

        await usersHelper.CreateRandomUserAsync();
    }
}
