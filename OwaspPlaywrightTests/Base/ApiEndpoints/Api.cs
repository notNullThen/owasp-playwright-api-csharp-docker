namespace OwaspPlaywrightTests.Base.ApiEndpoints;

public static class Api
{
    private static readonly AsyncLocal<SecurityAnswersApi> _securityAnswers = new();
    private static readonly AsyncLocal<RestUserApi> _restUser = new();
    private static readonly AsyncLocal<UsersApi> _users = new();

    public static SecurityAnswersApi SecurityAnswers =>
        _securityAnswers.Value ??= new SecurityAnswersApi();
    public static RestUserApi RestUser => _restUser.Value ??= new RestUserApi();
    public static UsersApi Users => _users.Value ??= new UsersApi();
}
