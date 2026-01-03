using OwaspPlaywrightTests.Base;
using OwaspPlaywrightTests.Base.Api;
using OwaspPlaywrightTests.Base.Api.Base.Types.User;
using OwaspPlaywrightTests.Data;

namespace OwaspPlaywrightTests.Support.Helpers;

public class UsersHelper
{
    public async Task<User> CreateUserAsync(UserPayload payload)
    {
        return await Test.StepAsync(
            $"Creating \"{payload.Email}\" user",
            async () =>
            {
                var userResponse = await Api.Users.PostUser(payload).RequestAsync();

                await Api
                    .SecurityAnswers.PostSecurityAnswers(
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
        );
    }

    public async Task<User> CreateRandomUserAsync()
    {
        var userPayload = UsersData.GenerateRandomUser();
        return await CreateUserAsync(userPayload);
    }
}
