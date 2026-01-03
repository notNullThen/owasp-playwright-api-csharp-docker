using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Base.Data;
using OwaspPlaywrightTests.Pages;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests.UI;

public class AuthorizationTest(ITestOutputHelper output) : Test(output)
{
    [Fact]
    public async Task UserCanRegisterSuccessfully()
    {
        var registrationPage = new RegistrationPage();
        var loginPage = new LoginPage();

        var generatedUser = UsersData.GenerateRandomUser();

        await registrationPage.GoToAsync();

        await Expect(registrationPage.RegisterButton).ToBeDisabledAsync();

        await registrationPage.RegisterUserAsync(
            email: generatedUser.Email,
            password: generatedUser.Password,
            securityQuestion: generatedUser.SecurityQuestion.Question,
            securityAnswer: generatedUser.SecurityAnswer
        );

        await loginPage.LoginAsync(email: generatedUser.Email, password: generatedUser.Password);
    }
}
