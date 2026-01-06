using System.Diagnostics;
using System.Runtime.CompilerServices;
using OwaspPlaywrightTests.Base;

namespace OwaspPlaywrightTests;

public static class TestConfig
{
    public const string PriceSymbol = "¤";
    public const int Timeout = 5_000;
    public const int ApiWaitTimeout = 5_000;
    public static readonly IReadOnlyCollection<int> ExpectedApiStatusCodes = [200, 201];
    public const string TestIdAttribute = "data-testid";
    public const int ViewportWidth = 1280;
    public const int ViewportHeight = 720;
    public static bool Headless => !Debugger.IsAttached;
    public static string BaseUrl =>
        string.IsNullOrWhiteSpace(EnvironmentVariables.CI())
            ? "http://localhost:3000"
            : "http://juice-shop:3000";

    public static int BaseUrlReadyRetries = 5;
    public static int BaseUrlReadyDelayMs = 1_000;

    public static string TracesDir =>
        Path.Combine("../", "../", "../", "../", "./playwright-traces");

    [ModuleInitializer]
    public static void Initialize()
    {
        DotNetEnv.Env.Load(File.OpenRead("../../../../.env"));
    }
}
