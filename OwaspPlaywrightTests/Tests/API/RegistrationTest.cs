using OwaspPlaywrightTests.Hooks;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Tests.API;

public class RegistrationTest(ITestOutputHelper outputHelper) : CreatedUserHook(outputHelper)
{
    [Fact(DisplayName = "User can register successfully")]
    public async Task UserCanRegisterSuccessfully()
    {
        var payloadData = CreatedUser.Payload;
        var responseData = CreatedUser.Response.Data;

        Assert.True(
            CreatedUser.Response.Status == "success",
            "User registration should be successful"
        );

        Assert.True(responseData.Id > 0, "Security question ID should be valid");

        Assert.True(
            responseData.Email.Equals(
                payloadData.Email,
                StringComparison.InvariantCultureIgnoreCase
            ),
            "Registered email should match the input email"
        );

        Assert.True(responseData.IsActive, "IsActive should be true for a newly registered user");

        /* Nulls and Empty checks */

        Assert.False(string.IsNullOrWhiteSpace(responseData.CreatedAt));

        Assert.True(string.IsNullOrEmpty(responseData.DeletedAt));

        Assert.False(string.IsNullOrWhiteSpace(responseData.LastLoginIp));

        Assert.False(string.IsNullOrWhiteSpace(responseData.ProfileImage));

        Assert.False(string.IsNullOrWhiteSpace(responseData.Role));

        Assert.False(string.IsNullOrWhiteSpace(responseData.UpdatedAt));
    }
}
