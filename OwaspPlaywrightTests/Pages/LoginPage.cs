using Microsoft.Playwright;
using OwaspPlaywrightTests.ApiEndpoints;
using OwaspPlaywrightTests.ApiEndpoints.Types.RestUserApi;
using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Base.ApiHandler.Types;
using OwaspPlaywrightTests.Components;

namespace OwaspPlaywrightTests.Pages;

public class LoginPage() : PageBase("/#/login")
{
    public InputFormField EmailInput =>
        new InputFormField(componentName: "Email").GetByAriaLabel("email");
    public InputFormField PasswordInput =>
        new InputFormField(componentName: "Password").GetByAriaLabel("password");
    public ILocator LoginButton =>
        Page.GetByRole(AriaRole.Button, new() { Name = "Login", Exact = true });
    public Checkbox RememberMeCheckbox =>
        new Checkbox(componentName: "Remember Me").GetByName("Remember me");

    public async Task<BrowserApiResponse<LoginResponse>> LoginAsync(
        string email,
        string password,
        bool rememberMe = true
    )
    {
        return await Test.StepAsync(
            $"Login with {email} user",
            async () =>
            {
                await EmailInput.FillAsync(email);
                await PasswordInput.FillAsync(password);
                if (rememberMe)
                {
                    await RememberMeCheckbox.CheckAsync();
                }

                var loginResponseTask = Api.RestUser.PostLogin().WaitAsync();
                await Task.WhenAll(LoginButton.ClickAsync(), loginResponseTask);

                await Header.AccountMenu.OpenAsync();
                await Assertions
                    .Expect(Header.AccountMenu.UserProfileItem)
                    .ToContainTextAsync(email);
                await Header.AccountMenu.CloseAsync();

                return await loginResponseTask;
            }
        );
    }
}
