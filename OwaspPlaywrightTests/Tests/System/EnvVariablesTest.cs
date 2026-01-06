using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests.Tests.System;

public class EnvVariablesTest
{
    [Fact]
    public void EnvVariablesAreLoadedSuccessfully()
    {
        var adminUserEmail = CIEnvironmentVariables.ADMIN_USER_EMAIL();
        var adminUserPassword = CIEnvironmentVariables.ADMIN_USER_PASSWORD();

        Assert.False(string.IsNullOrWhiteSpace(adminUserEmail));
        Assert.False(string.IsNullOrWhiteSpace(adminUserPassword));
    }
}
