using System;
using System.IO;
using System.Reflection;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Support
{
    public abstract class PlaywrightTestBase : PlaywrightTest
    {
        private IBrowser? _browser;
        public IPage Page { get; private set; } = null!;
        public ITestOutputHelper? Output { get; private set; }

        public PlaywrightTestBase(ITestOutputHelper output)
        {
            Output = output;
        }

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
                var testName = GetTestName();

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

        private string GetTestName()
        {
            if (Output != null)
            {
                var type = Output.GetType();
                var testMember = type.GetField(
                    "test",
                    BindingFlags.Instance | BindingFlags.NonPublic
                );

                if (testMember != null)
                {
                    var test = (ITest?)testMember.GetValue(Output);
                    if (test != null)
                    {
                        return string.Join(" - ", test.DisplayName.Split('.').Skip(1));
                    }
                }
            }

            return $"UnknownTest_{Guid.NewGuid()}";
        }
    }
}
