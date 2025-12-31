using System.Diagnostics;
using OwaspPlaywrightTests.Support;

namespace OwaspPlaywrightTests;

public static class PlaywrightConfig
{
    public const int Timeout = 5 * 1000;
    public const int ApiWaitTimeout = 5 * 1000;
    public static readonly int[] ExpectedAPIResponseCodes = [200, 201];
    public const string TestIdAttribute = "data-testid";
    public const int ViewportWidth = 1280;
    public const int ViewportHeight = 720;
    public static bool Headless => !Debugger.IsAttached;
    public static string BaseURL =>
        string.IsNullOrWhiteSpace(EnvironmentVariables.CI())
            ? "http://localhost:3000"
            : "http://juice-shop:3000";

    public static string TracesDir =>
        Path.Combine("../", "../", "../", "../", "./playwright-traces");
}
