using OwaspPlaywrightTests.ApiEndpoints;
using OwaspPlaywrightTests.Hooks;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests.API;

public class AuthenticationTest(ITestOutputHelper outputHelper) : CreatedUserHook(outputHelper)
{
    [Fact(DisplayName = "User can authenticate successfully")]
    public async Task UserCanAuthenticateSuccessfully()
    {
        var response = await Api
            .RestUser.PostLogin(
                new() { Email = CreatedUser.Payload.Email, Password = CreatedUser.Payload.Password }
            )
            .RequestAsync();

        var loginResponse = response.ResponseBody!.Authentication;

        Assert.NotNull(response.ResponseBody);
        Assert.NotNull(loginResponse.Umail);

        Assert.NotEmpty(loginResponse.Token);

        Assert.NotEqual(CreatedUser.Payload.Email, loginResponse.Umail);

        Assert.True(loginResponse.Bid > 0, "User ID (bid) should be a positive integer");
    }
}
