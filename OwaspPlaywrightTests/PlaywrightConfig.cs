using System.Diagnostics;
using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests
{
    public static class PlaywrightConfig
    {
        public const int Timeout = 5 * 1000;
        public const string TestIdAttribute = "data-testid";
        public static bool Headless => !Debugger.IsAttached;
        public static string BaseURL =>
            string.IsNullOrWhiteSpace(EnvironmentVariables.CI())
                ? "http://localhost:3000"
                : "http://juice-shop:3000";

        public static string TracesDir => "./playwright-traces";
    }
}
