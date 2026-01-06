using OwaspPlaywrightTests.ApiEndpoints.Types.RestUserApi;
using OwaspPlaywrightTests.Base.ApiHandler;
using OwaspPlaywrightTests.Pages;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Hooks;

public abstract class AuthenticatedHook(ITestOutputHelper output) : CreatedUserHook(output)
{
    protected LoginResponse LoggedInUserResponse { get; private set; } = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        var loginPage = new LoginPage();
        await loginPage.GotoAsync();
        var loginResponse = await loginPage.LoginAsync(
            email: CreatedUser.Payload.Email,
            password: CreatedUser.Payload.Password
        );
        LoggedInUserResponse = loginResponse.ResponseBody!;

        ApiParametersBase.SetToken($"Bearer {LoggedInUserResponse.Authentication.Token}");
    }
}
