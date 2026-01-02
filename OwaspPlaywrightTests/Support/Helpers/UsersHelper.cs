using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Base.Api;
using OwaspPlaywrightTests.Base.Api.Base.Types.User;
using OwaspPlaywrightTests.Base.Data;

namespace OwaspPlaywrightTests.Support.Helpers;

public class UsersHelper
{
    private readonly UsersApi _usersApi = new();
    private readonly SecurityAnswersApi _securityAnswersApi = new();

    public async Task<User> CreateUserAsync(UserPayload payload)
    {
        var userResponse = await _usersApi.PostUser(payload).RequestAsync();
        ;
        await _securityAnswersApi
            .PostSecurityAnswers(
                new()
                {
                    UserId = userResponse.ResponseBody!.Data.Id,
                    SecurityQuestionId = payload.SecurityQuestion.Id,
                    Answer = payload.SecurityAnswer,
                }
            )
            .RequestAsync();

        var user = new User { Payload = payload, Response = userResponse.ResponseBody };
        CreatedData.CreatedUsers.Add(user);

        return user;
    }

    public async Task<User> CreateRandomUserAsync()
    {
        var userPayload = UsersData.GenerateRandomUser();

        await TestContext.StartTracingGroupAsync($"Creating \"{userPayload.Email}\" user");
        var user = await CreateUserAsync(userPayload);
        await TestContext.EndTracingGroupAsync();

        return user;
    }
}
