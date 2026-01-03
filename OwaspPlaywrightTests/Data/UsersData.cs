using Bogus;
using OwaspPlaywrightTests.Base.Api.Base.Types.User;

namespace OwaspPlaywrightTests.Data;

public static class UsersData
{
    public static UserPayload GenerateRandomUser()
    {
        var faker = new Faker();
        var password = faker.Internet.Password();
        var now = DateTime.UtcNow.ToString("o");

        var user = new UserPayload
        {
            Email = faker.Internet.Email(),
            Password = password,
            PasswordRepeat = password,
            SecurityAnswer = faker.Lorem.Word(),
            SecurityQuestion = new()
            {
                Id = 1,
                Question = "Your eldest siblings middle name?",
                CreatedAt = now,
                UpdatedAt = now,
            },
        };

        return user;
    }
}
