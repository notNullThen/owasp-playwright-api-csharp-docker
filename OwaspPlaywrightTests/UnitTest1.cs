using System.Text.Json;
using OwaspPlaywrightTests.Support;
using OwaspPlaywrightTests.Support.Api;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests;

public class UnitTest1(ITestOutputHelper output) : TestContext(output)
{
    [Fact]
    public async Task Test1()
    {
        var usersApi = new UsersApi();
        var securityAnswersApi = new SecurityAnswersApi();

        var response = await usersApi
            .PostUser(
                new()
                {
                    Email = "test@test.com",
                    Password = "12345678",
                    PasswordRepeat = "12345678",
                    SecurityQuestion = new()
                    {
                        Id = 1,
                        Question = "Your eldest siblings middle name?",
                        CreatedAt = "2025-12-31T15:03:02.655Z",
                        UpdatedAt = "2025-12-31T15:03:02.655Z",
                    },
                    SecurityAnswer = "asdasd",
                }
            )
            .Request();

        var userId = response.ResponseBody!.Data.Id;

        await securityAnswersApi
            .PostSecurityAnswers(
                new()
                {
                    Answer = "asdasd",
                    SecurityQuestionId = 1,
                    UserId = userId,
                }
            )
            .Request();
    }
}
