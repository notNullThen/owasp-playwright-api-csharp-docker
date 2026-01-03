using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Base.Data;
using OwaspPlaywrightTests.Pages;
using OwaspPlaywrightTests.Support.Helpers;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests.UI;

public class AuthorizationTest
{
    public class UserRegistrationTests(ITestOutputHelper output) : Test(output)
    {
        [Fact]
        public async Task UserCanRegisterSuccessfully()
        {
            var registrationPage = new RegistrationPage();

            var generatedUser = UsersData.GenerateRandomUser();

            await registrationPage.GoToAsync();

            await Expect(registrationPage.RegisterButton).ToBeDisabledAsync();

            await registrationPage.RegisterUserAsync(
                email: generatedUser.Email,
                password: generatedUser.Password,
                securityQuestion: generatedUser.SecurityQuestion.Question,
                securityAnswer: generatedUser.SecurityAnswer
            );
        }
    }

    public class LoginTests(ITestOutputHelper output) : Test(output)
    {
        [Fact]
        public async Task UserCanLogInSuccessfully()
        {
            var loginPage = new LoginPage();

            var preparedUser = await new UsersHelper().CreateRandomUserAsync();
            await loginPage.GoToAsync();

            await loginPage.LoginAsync(
                email: preparedUser.Payload.Email,
                password: preparedUser.Payload.Password
            );
        }
    }
}
