using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests.Fixtures;

public static class GlobalSetup
{
    public static async Task GlobalSetupAsync()
    {
        await TestUtils.DismissCookiesAsync();
        await TestUtils.DismissWelcomeBannerAsync();
    }
}
