using OwaspPlaywrightTests.Pages;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Hooks;

public class AuthenticatedHook(ITestOutputHelper output) : CreatedUserHook(output)
{
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        var loginPage = new LoginPage();
        await loginPage.GotoAsync();
        await loginPage.LoginAsync(
            email: CreatedUser.Payload.Email,
            password: CreatedUser.Payload.Password
        );
    }
}
