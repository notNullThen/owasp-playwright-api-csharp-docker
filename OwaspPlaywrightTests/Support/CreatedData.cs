using OwaspPlaywrightTests.ApiEndpoints.Types.User;

namespace OwaspPlaywrightTests.Support;

public static class CreatedData
{
    public static List<User> CreatedUsers = [];

    public static User GetUserByEmail(string email)
    {
        return CreatedUsers.First(user =>
            user.Response.Data.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase)
        );
    }
}
