#pragma warning disable CS8603 // Possible null reference return.

namespace OwaspPlaywrightTests.Support
{
    public static class EnvironmentVariables
    {
        public static string CI() => Environment.GetEnvironmentVariable("CI");
    }
}
