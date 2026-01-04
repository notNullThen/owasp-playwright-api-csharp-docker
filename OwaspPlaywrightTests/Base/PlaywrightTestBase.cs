using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using OwaspPlaywrightTests.Fixtures;

namespace OwaspPlaywrightTests.Base;

public abstract class PlaywrightTestBase : PlaywrightTest
{
    private IBrowser? _browser;
    public IPage Page { get; private set; } = null!;
    public IAPIRequestContext Request => Page.APIRequest;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync().ConfigureAwait(false);

        Playwright.Selectors.SetTestIdAttribute(TestConfig.TestIdAttribute);
        _browser = await Playwright.Chromium.LaunchAsync(new() { Headless = TestConfig.Headless });

        var context = await _browser.NewContextAsync(
            new()
            {
                BaseURL = TestConfig.BaseURL,
                ViewportSize = new()
                {
                    Width = TestConfig.ViewportWidth,
                    Height = TestConfig.ViewportHeight,
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
        Page.SetDefaultTimeout(TestConfig.Timeout);
        Page.SetDefaultNavigationTimeout(TestConfig.Timeout);

        Test.Page = Page;

        await GlobalSetup.GlobalSetupAsync();
    }

    public override async Task DisposeAsync()
    {
        if (Page != null)
        {
            var testName = Test.GetTestName();

            Directory.CreateDirectory(TestConfig.TracesDir);
            await Page.Context.Tracing.StopAsync(
                new() { Path = Path.Combine(TestConfig.TracesDir, $"{testName}.zip") }
            );
        }

        if (_browser != null)
        {
            await _browser.CloseAsync();
        }
    }
}
