using OwaspPlaywrightTests.Base.Api.Base.Types.User;
using OwaspPlaywrightTests.Support.Helpers;

namespace OwaspPlaywrightTests.Fixtures;

public class PrepareRandomUserFixture
{
    /// <summary>
    /// Stores users created as a fixture for each parallel test run.
    /// </summary>
    /// <remarks>
    /// Key: <c>ThreadID</c> which is <c>Environment.CurrentManagedThreadId</c><br/>
    /// Value: <c>User</c> object.
    /// </remarks>
    public static Dictionary<int, User> PreparedUser = [];

    public PrepareRandomUserFixture()
    {
        var user = new UsersHelper().CreateRandomUserAsync().GetAwaiter().GetResult();
        PreparedUser[Environment.CurrentManagedThreadId] = user;
    }

    public static User GetPreparedUser()
    {
        return PreparedUser[Environment.CurrentManagedThreadId];
    }
}
