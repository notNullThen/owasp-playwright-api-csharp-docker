using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests.Fixtures;

public static class GlobalSetup
{
    public static async Task GlobalSetupAsync()
    {
        await Utils.DismissCookiesAsync();
        await Utils.DismissWelcomeBannerAsync();
    }
}
