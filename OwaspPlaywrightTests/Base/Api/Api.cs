namespace OwaspPlaywrightTests.Base.Api;

public static class Api
{
    private static readonly AsyncLocal<SecurityAnswersApi> _securityAnswers = new();
    private static readonly AsyncLocal<RestUserApi> _restUsers = new();
    private static readonly AsyncLocal<UsersApi> _users = new();

    public static SecurityAnswersApi SecurityAnswers =>
        _securityAnswers.Value ??= new SecurityAnswersApi();
    public static RestUserApi RestUsers => _restUsers.Value ??= new RestUserApi();
    public static UsersApi Users => _users.Value ??= new UsersApi();
}
