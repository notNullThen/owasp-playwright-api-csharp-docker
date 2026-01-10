using Allure.Net.Commons;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using OwaspPlaywrightTests.Tests.System;

namespace OwaspPlaywrightTests.Base;

public abstract class PlaywrightTestBase : PlaywrightTest
{
    private IBrowser? _browser;
    public IPage Page { get; private set; } = null!;
    public IAPIRequestContext Request => Page.APIRequest;

    public override async Task InitializeAsync()
    {
        EnvVariablesTest.EnvVariablesAreLoadedSuccessfully();

        await base.InitializeAsync().ConfigureAwait(false);

        Playwright.Selectors.SetTestIdAttribute(TestConfig.TestIdAttribute);
        _browser = await Playwright.Chromium.LaunchAsync(new() { Headless = TestConfig.Headless });

        var context = await _browser.NewContextAsync(
            new()
            {
                BaseURL = TestConfig.BaseUrl,
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
            var tracePath = Path.Combine(TestConfig.TracesDir, $"{testName}.zip");

            Directory.CreateDirectory(TestConfig.TracesDir);
            await Page.Context.Tracing.StopAsync(new() { Path = tracePath });

            AllureApi.AddAttachment(
                "Playwright Trace",
                "application/vnd.allure.playwright-trace",
                tracePath
            );
        }

        if (_browser != null)
        {
            await _browser.CloseAsync();
        }
    }
}
