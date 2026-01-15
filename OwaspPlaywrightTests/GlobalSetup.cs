using OwaspPlaywrightTests.Base.ApiHandler;
using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests;

public static class GlobalSetup
{
    private static bool _initialized;
    private static readonly SemaphoreSlim _semaphore = new(1, 1);

    public static async Task GlobalSetupAsync()
    {
        if (!_initialized)
        {
            await _semaphore.WaitAsync();
            try
            {
                if (!_initialized)
                {
                    ApiParametersBase.SetInitialConfig(
                        apiWaitTimeout: TestConfig.ApiWaitTimeout,
                        expectedStatusCodes: TestConfig.ExpectedApiStatusCodes,
                        baseUrl: TestConfig.BaseUrl
                    );

                    await TestUtils.WaitForBaseUrlReadyAsync();

                    _initialized = true;
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        await TestUtils.DismissCookiesAsync();
        await TestUtils.DismissWelcomeBannerAsync();
    }
}
