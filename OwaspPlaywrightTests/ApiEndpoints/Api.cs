using OwaspPlaywrightTests.Base;
using SimpleApiPlaywright;

namespace OwaspPlaywrightTests.ApiEndpoints;

public static class Api
{
    private static T Create<T>(T instance)
    {
        ApiClient.SetContext(new ApiContext(Test.Page.Context, Test.Page, Test.Page.APIRequest));
        return instance;
    }

    public static SecurityAnswersApi SecurityAnswers => Create(new SecurityAnswersApi());
    public static RestUserApi RestUser => Create(new RestUserApi());
    public static UsersApi Users => Create(new UsersApi());
    public static BasketItemsApi BasketItems => Create(new BasketItemsApi());
    public static ProductsApi Products => Create(new ProductsApi());
    public static RestBasketApi RestBasket => Create(new RestBasketApi());
}
