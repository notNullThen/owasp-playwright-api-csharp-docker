using OwaspPlaywrightTests.Base.ApiHandler;
using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests.Fixtures;

public static class GlobalSetup
{
    public static async Task GlobalSetupAsync()
    {
        ApiParametersBase.SetInitialConfig(
            apiWaitTimeout: TestConfig.ApiWaitTimeout,
            expectedStatusCodes: TestConfig.ExpectedApiStatusCodes,
            baseUrl: TestConfig.BaseUrl
        );

        await TestUtils.DismissCookiesAsync();
        await TestUtils.DismissWelcomeBannerAsync();
    }
}
