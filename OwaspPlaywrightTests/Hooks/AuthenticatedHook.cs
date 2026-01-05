using OwaspPlaywrightTests.ApiEndpoints.Types.RestUserApi;
using OwaspPlaywrightTests.Base.ApiHandler;
using OwaspPlaywrightTests.Pages;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Hooks;

public class AuthenticatedHook(ITestOutputHelper output) : CreatedUserHook(output)
{
    protected LoginResponse LoginResponseBody { get; private set; } = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        var loginPage = new LoginPage();
        await loginPage.GotoAsync();
        var loginResponse = await loginPage.LoginAsync(
            email: CreatedUser.Payload.Email,
            password: CreatedUser.Payload.Password
        );
        LoginResponseBody = loginResponse.ResponseBody!;

        ApiParametersBase.SetToken($"Bearer {LoginResponseBody.Authentication.Token}");
    }
}
