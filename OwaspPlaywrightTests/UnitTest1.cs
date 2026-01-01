using System.Text.Json;
using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Base.Api;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests;

public class UnitTest1(ITestOutputHelper output) : TestContext(output)
{
    [Fact]
    public async Task Test1()
    {
        var usersApi = new UsersApi();
        var securityAnswersApi = new SecurityAnswersApi();

        var now = DateTime.UtcNow.ToString("O");
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
                        CreatedAt = now,
                        UpdatedAt = now,
                    },
                    SecurityAnswer = "asdasd",
                }
            )
            .RequestAsync();

        var userId = response.ResponseBody!.Data.Id;

        var answerResponse = await securityAnswersApi
            .PostSecurityAnswers(
                new()
                {
                    Answer = "asdasd",
                    SecurityQuestionId = 1,
                    UserId = userId,
                }
            )
            .RequestAsync();
    }
}
