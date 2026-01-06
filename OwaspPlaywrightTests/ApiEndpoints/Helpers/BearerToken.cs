namespace OwaspPlaywrightTests.ApiEndpoints.Helpers;

public static class BearerToken
{
    public static string Format(string token)
    {
        token = token.Trim();
        return token.StartsWith("Bearer ") ? token : $"Bearer {token}";
    }
}
