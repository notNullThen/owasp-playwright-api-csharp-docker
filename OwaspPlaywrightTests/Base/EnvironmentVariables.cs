#pragma warning disable CS8603 // Possible null reference return.

namespace OwaspPlaywrightTests.Base;

public static class EnvironmentVariables
{
    public static string CI() => Environment.GetEnvironmentVariable("CI");

    public static string ADMIN_USER_EMAIL() => GetValue("ADMIN_USER_EMAIL");

    public static string ADMIN_USER_PASSWORD() => GetValue("ADMIN_USER_PASSWORD");

    private static string GetValue(string variableName)
    {
        var value = Environment.GetEnvironmentVariable(variableName);
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException(
                $"Environment variable '{variableName}' is not set."
            );
        }
        return value;
    }
}
