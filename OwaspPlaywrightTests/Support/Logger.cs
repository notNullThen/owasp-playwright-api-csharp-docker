using Xunit.Abstractions;

namespace OwaspPlaywrightTests.Support
{
    public class Logger(ITestOutputHelper output)
    {
        public void Log(string message)
        {
            output.WriteLine($"[LOG] {message}");
        }
    }
}
