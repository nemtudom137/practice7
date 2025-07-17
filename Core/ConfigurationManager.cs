using Microsoft.Extensions.Configuration;
using NLog;

namespace Core;

public static class ConfigurationManager
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    private static TestConfiguration? test;

    public static TestConfiguration Test
    {
        get
        {
            test ??= GetConfiguration();
            return test;
        }
    }

    public static void SetDownloadFolder()
    {
        var download = Test.DownloadDirectory;
        if (Directory.Exists(download))
        {
            Directory.Delete(download, true);
        }

        Directory.CreateDirectory(download);
    }

    public static void SetScreenshotFolder()
    {
        var screenshots = Test.ScreenshotDirectory;

        if (!Directory.Exists(screenshots))
        {
            Directory.CreateDirectory(screenshots);
        }
    }

    private static TestConfiguration GetConfiguration()
    {
        var config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();

        var testConfig = config.GetSection("Test").Get<TestConfiguration>();

        Log.Info($"Config: {testConfig?.Url}, {testConfig?.Browser}, headless: {testConfig?.Headless}, timeout: {testConfig?.ExplicitTimeoutSec}, download: {testConfig?.DownloadDirectory}, screenshots: {testConfig?.ScreenshotDirectory}");

        return testConfig ?? throw new ArgumentException(nameof(testConfig));
    }
}