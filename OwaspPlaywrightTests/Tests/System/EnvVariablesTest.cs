using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Tests.System;

public class EnvVariablesTest
{
    [Fact(DisplayName = "Environment variables are loaded successfully")]
    public static void EnvVariablesAreLoadedSuccessfully()
    {
        CIEnvironmentVariables.ADMIN_USER_EMAIL();
        CIEnvironmentVariables.ADMIN_USER_PASSWORD();
    }
}
