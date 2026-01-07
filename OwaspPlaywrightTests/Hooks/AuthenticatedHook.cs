using OwaspPlaywrightTests.ApiEndpoints;
using OwaspPlaywrightTests.ApiEndpoints.Helpers;
using OwaspPlaywrightTests.ApiEndpoints.Types.RestUserApi;
using OwaspPlaywrightTests.Base.ApiHandler;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Hooks;

public abstract class AuthenticatedHook(ITestOutputHelper output) : CreatedUserHook(output)
{
    protected LoginResponse LoggedInUserResponse { get; private set; } = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        var loginResponse = await Api
            .RestUser.PostLogin(
                new() { Email = CreatedUser.Payload.Email, Password = CreatedUser.Payload.Password }
            )
            .RequestAsync();

        LoggedInUserResponse = loginResponse.ResponseBody!;

        ApiParametersBase.SetToken(BearerToken.Format(LoggedInUserResponse.Authentication.Token));
    }
}
