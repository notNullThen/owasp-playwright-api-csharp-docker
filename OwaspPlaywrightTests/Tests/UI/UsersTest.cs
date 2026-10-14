using OwaspPlaywrightTests.ApiEndpoints;
using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Hooks;
using OwaspPlaywrightTests.Pages;
using OwaspPlaywrightTests.Support.Helpers;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests.UI;

public class UsersTest
{
    [Trait("Suite", "UI")]
    public class RegistrationTests(ITestOutputHelper outputHelper) : Test(outputHelper)
    {
        [Fact(DisplayName = "User can register successfully")]
        public async Task UserCanRegisterSuccessfully()
        {
            var registrationPage = new RegistrationPage();

            var generatedUser = UsersHelper.GenerateRandomUser();

            await registrationPage.GotoAsync();

            await Expect(registrationPage.RegisterButton).ToBeDisabledAsync();

            await registrationPage.RegisterUserAsync(
                email: generatedUser.Email,
                password: generatedUser.Password,
                securityQuestion: generatedUser.SecurityQuestion.Question,
                securityAnswer: generatedUser.SecurityAnswer
            );
        }
    }

    [Trait("Suite", "UI")]
    public class LogInTests(ITestOutputHelper outputHelper) : CreatedUserHook(outputHelper)
    {
        [Fact(DisplayName = "User can log in successfully")]
        public async Task UserCanLogInSuccessfully()
        {
            var loginPage = new LoginPage();

            await loginPage.GotoAsync();
            await loginPage.LoginAsync(
                email: CreatedUser.Payload.Email,
                password: CreatedUser.Payload.Password
            );
        }

        [Fact(DisplayName = "Login completes only after the held API response is released")]
        public async Task LoginCompletesOnlyAfterHeldResponseIsReleased()
        {
            var loginPage = new LoginPage();
            var email = CreatedUser.Payload.Email;

            await loginPage.GotoAsync();
            await loginPage.EmailInput.FillAsync(email);
            await loginPage.PasswordInput.FillAsync(CreatedUser.Payload.Password);

            var holdTask = Api.RestUser.PostLogin().HoldAsync();
            await loginPage.LoginButton.ClickAsync();
            await using var heldResponse = await holdTask;

            // The server has responded, but the browser is still on the login form.
            await Expect(loginPage.LoginButton).ToBeVisibleAsync();
            await Expect(loginPage.EmailInput.Input).ToHaveValueAsync(email);

            await heldResponse.ReleaseAsync(failIfNotReleased: true);

            await Expect(loginPage.LoginButton).ToBeHiddenAsync();
            await loginPage.Header.AccountMenu.OpenAsync();
            await Expect(loginPage.Header.AccountMenu.UserProfileItem).ToContainTextAsync(email);
        }
    }
}
