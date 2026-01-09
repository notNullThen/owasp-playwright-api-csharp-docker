using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Hooks;
using OwaspPlaywrightTests.Pages;
using OwaspPlaywrightTests.Support.Helpers;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests.UI;

public class UsersTest
{
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
    }
}
