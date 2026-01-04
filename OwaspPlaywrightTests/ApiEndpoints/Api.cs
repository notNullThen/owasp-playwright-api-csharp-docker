namespace OwaspPlaywrightTests.ApiEndpoints;

public static class Api
{
    private static readonly AsyncLocal<SecurityAnswersApi> _securityAnswers = new();
    private static readonly AsyncLocal<RestUserApi> _restUser = new();
    private static readonly AsyncLocal<UsersApi> _users = new();
    private static readonly AsyncLocal<BasketItemsApi> _basketItems = new();

    public static SecurityAnswersApi SecurityAnswers =>
        _securityAnswers.Value ??= new SecurityAnswersApi();
    public static RestUserApi RestUser => _restUser.Value ??= new RestUserApi();
    public static UsersApi Users => _users.Value ??= new UsersApi();
    public static BasketItemsApi BasketItems => _basketItems.Value ??= new BasketItemsApi();
}
