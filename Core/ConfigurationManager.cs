using Microsoft.Extensions.Configuration;

namespace Core;

public static class ConfigurationManager
{
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
        var download = Test.DirectoryForDownload;
        if (Directory.Exists(download))
        {
            Directory.Delete(download, true);
        }

        Directory.CreateDirectory(download);
    }

    public static void SetScreenshotFolder()
    {
        var screenshots = Test.DirectoryForScreenshots;

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

        var testConfig = new TestConfiguration(config.GetSection("Test"));
        LogHelper.Info($"Config: {testConfig.Url}, {testConfig.Browser}, headless: {testConfig.Headless}, timeout: {testConfig.ExplicitTimeoutSec}, download: {testConfig.DirectoryForDownload}, screenshots: {testConfig.DirectoryForScreenshots}");

        return testConfig;
    }
}