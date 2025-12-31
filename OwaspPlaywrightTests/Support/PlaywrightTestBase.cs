using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;

namespace OwaspPlaywrightTests.Support;

public abstract class PlaywrightTestBase : PlaywrightTest
{
    private IBrowser? _browser;
    public IPage Page { get; private set; } = null!;
    public IAPIRequestContext Request => Page.APIRequest;

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
                ViewportSize = new()
                {
                    Width = PlaywrightConfig.ViewportWidth,
                    Height = PlaywrightConfig.ViewportHeight,
                },
            }
        );

        await context.Tracing.StartAsync(
            new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true,
            }
        );

        Page = await context.NewPageAsync();
    }

    public override async Task DisposeAsync()
    {
        if (Page != null)
        {
            var testName = TestContext.GetTestName();

            Directory.CreateDirectory(PlaywrightConfig.TracesDir);
            await Page.Context.Tracing.StopAsync(
                new() { Path = Path.Combine(PlaywrightConfig.TracesDir, $"{testName}.zip") }
            );
        }

        if (_browser != null)
        {
            await _browser.CloseAsync();
        }
    }
}
