using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;

namespace OwaspPlaywrightTests.Support
{
    public abstract class PlaywrightDriver : PlaywrightTest
    {
        private IBrowser? _browser;
        public IPage Page { get; private set; } = null!;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync().ConfigureAwait(false);

            Playwright.Selectors.SetTestIdAttribute(PlaywrightConfig.TestIdAttribute);
            _browser = await Playwright.Chromium.LaunchAsync(
                new()
                {
                    Headless = PlaywrightConfig.Headless,
                    Timeout = PlaywrightConfig.Timeout,
                    TracesDir = PlaywrightConfig.TracesDir,
                }
            );

            var context = await _browser.NewContextAsync(
                new()
                {
                    BaseURL = PlaywrightConfig.BaseURL,
                    ViewportSize = new() { Width = 1280, Height = 720 },
                }
            );

            Page = await context.NewPageAsync();
        }

        public override async Task DisposeAsync()
        {
            if (_browser != null)
            {
                await _browser.CloseAsync();
            }
        }
    }
}
