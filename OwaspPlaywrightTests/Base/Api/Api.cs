namespace OwaspPlaywrightTests.Base.Api;

public static class Api
{
    public static SecurityAnswersApi SecurityAnswers => new();
    public static RestUserApi RestUsers => new();
    public static UsersApi Users => new();
}
