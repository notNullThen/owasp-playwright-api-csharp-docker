using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Data;

public struct User
{
    public string Email { get; init; }
    public string Password { get; init; }
}

public static class UsersTestData
{
    public static readonly User AdminUser = new()
    {
        Email = CIEnvironmentVariables.ADMIN_USER_EMAIL(),
        Password = CIEnvironmentVariables.ADMIN_USER_PASSWORD(),
    };
}
