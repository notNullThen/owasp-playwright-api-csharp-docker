using OwaspPlaywrightTests.ApiEndpoints.Types.User;
using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Support.Helpers;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Hooks;

public class CreatedUserHook(ITestOutputHelper output) : Test(output)
{
    protected User CreatedUser { get; private set; } = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        CreatedUser = await new UsersHelper().CreateRandomUserAsync();
    }
}
