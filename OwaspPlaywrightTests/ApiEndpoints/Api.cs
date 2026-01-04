namespace OwaspPlaywrightTests.ApiEndpoints;

public static class Api
{
    private static readonly AsyncLocal<SecurityAnswersApi> _securityAnswers = new();
    private static readonly AsyncLocal<RestUserApi> _restUser = new();
    private static readonly AsyncLocal<UsersApi> _users = new();
    private static readonly AsyncLocal<BasketItemsApi> _basketItems = new();
    private static readonly AsyncLocal<RestBasketApi> _restBasket = new();
    private static readonly AsyncLocal<ProductsApi> _products = new();

    public static SecurityAnswersApi SecurityAnswers =>
        _securityAnswers.Value ??= new SecurityAnswersApi();
    public static RestUserApi RestUser => _restUser.Value ??= new RestUserApi();
    public static UsersApi Users => _users.Value ??= new UsersApi();
    public static BasketItemsApi BasketItems => _basketItems.Value ??= new BasketItemsApi();
    public static ProductsApi Products => _products.Value ??= new ProductsApi();
    public static RestBasketApi RestBasket => _restBasket.Value ??= new RestBasketApi();
}
