using Core.API;
using Core.UI;
using Microsoft.Extensions.Configuration;

namespace Core;

public static class ConfigurationManager
{
    private static IConfigurationRoot config = GetConfiguration();
    private static UITestConfiguration? ui;
    private static APITestConfiguration? api;

    public static UITestConfiguration UI
    {
        get
        {
            ui ??= config.GetSection("UI").Get<UITestConfiguration>();
            LogHelper.Log.Info($"Config: {ui?.Url}, {ui?.Browser}, headless: {ui?.Headless}, timeout: {ui?.ExplicitTimeoutSec}, download: {ui?.DownloadDirectory}, screenshots: {ui?.ScreenshotDirectory}");
            return ui ?? throw new ArgumentException(nameof(UI));
        }
    }

    public static APITestConfiguration API
    {
        get
        {
            api ??= config.GetSection("API").Get<APITestConfiguration>();
            LogHelper.Log.Info($"Config: {api?.Url}");
            return api ?? throw new ArgumentException(nameof(API));
        }
    }

    public static void SetDownloadFolder()
    {
        var download = UI.DownloadDirectory;
        if (Directory.Exists(download))
        {
            Directory.Delete(download, true);
        }

        Directory.CreateDirectory(download);
    }

    public static void SetScreenshotFolder()
    {
        var screenshots = UI.ScreenshotDirectory;

        if (!Directory.Exists(screenshots))
        {
            Directory.CreateDirectory(screenshots);
        }
    }

    private static IConfigurationRoot GetConfiguration()
    {
        return new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();
    }
}