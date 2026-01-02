using Microsoft.Playwright;
using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Base.Api;
using OwaspPlaywrightTests.Base.Api.Base.Types;
using OwaspPlaywrightTests.Base.Api.Base.Types.User;
using OwaspPlaywrightTests.Components;
using OwaspPlaywrightTests.Pages;

public class RegistrationPage() : PageBase("/#/register")
{
    public InputFormField EmailInput =>
        new InputFormField(componentName: "Email").GetByLocator("#emailControl");
    public InputFormField PasswordInput =>
        new InputFormField(componentName: "Password").GetByLocator("#passwordControl");
    public InputFormField RepeatPasswordInput =>
        new InputFormField(componentName: "Repeat Password").GetByLocator("#repeatPasswordControl");
    public DropdownFormField SecurityQuestionDropdown =>
        new DropdownFormField(componentName: "Security question").GetByName("security question");
    public InputFormField AnswerInput =>
        new InputFormField(componentName: "Security Answer").GetByLocator("#securityAnswerControl");
    public ILocator RegisterButton => Page.Locator("button[type=submit]");

    public async Task RegisterUserAsync(
        string email,
        string password,
        string securityQuestion,
        string securityAnswer
    )
    {
        await Test.StepAsync(
            $"Fill {email} user registration form",
            async () =>
            {
                await EmailInput.FillAsync(email);
                await EmailInput.ShouldNotHaveErrorAsync();

                await PasswordInput.FillAsync(password);
                await PasswordInput.ShouldNotHaveErrorAsync();

                await RepeatPasswordInput.FillAsync(password);
                await RepeatPasswordInput.ShouldNotHaveErrorAsync();

                await SecurityQuestionDropdown.SelectAsync(securityQuestion);

                await AnswerInput.FillAsync(securityAnswer);
                await AnswerInput.ShouldNotHaveErrorAsync();

                await SubmitAsync();
            }
        );
    }

    public async Task<BrowserApiResponse<UserResponse>> SubmitAsync()
    {
        return await Test.StepAsync(
            "Submit registration form",
            async () =>
            {
                var userResponseTask = Api.Users.PostUser().WaitAsync();
                await Task.WhenAll(
                    RegisterButton.ClickAsync(),
                    userResponseTask,
                    Api.SecurityAnswers.PostSecurityAnswers().WaitAsync()
                );
                await Page.WaitForURLAsync("**/#/login");

                return await userResponseTask;
            }
        );
    }
}
