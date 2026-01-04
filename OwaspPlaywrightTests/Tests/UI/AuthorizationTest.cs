using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Data;
using OwaspPlaywrightTests.Hooks;
using OwaspPlaywrightTests.Pages;
using OwaspPlaywrightTests.Support.Helpers;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests.UI;

public class AuthorizationTest
{
    public class RegistrationTests(ITestOutputHelper outputHelper) : Test(outputHelper)
    {
        [Fact]
        public async Task UserCanRegisterSuccessfully()
        {
            var registrationPage = new RegistrationPage();

            var generatedUser = UsersData.GenerateRandomUser();

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

    public class LogIntests(ITestOutputHelper outputHelper) : CreatedUserHook(outputHelper)
    {
        [Fact]
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
