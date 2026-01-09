using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Tests.System;

public class EnvVariablesTest
{
    [Fact]
    public void EnvVariablesAreLoadedSuccessfully()
    {
        CIEnvironmentVariables.ADMIN_USER_EMAIL();
        CIEnvironmentVariables.ADMIN_USER_PASSWORD();
    }
}
