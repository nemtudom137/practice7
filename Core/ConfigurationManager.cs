using Microsoft.Extensions.Configuration;

namespace Core;

public static class ConfigurationManager
{
    private static readonly string UISection = "UI";
    private static IConfigurationRoot config = GetConfiguration();
    private static UiTestConfiguration? ui;

    public static UiTestConfiguration UI
    {
        get
        {
            if (ui is null)
            {
                ui = config.GetSection(UISection).Get<UiTestConfiguration>();
                LogHelper.Log.Info($"Config: {ui?.Url}, {ui?.Browser}, headless: {ui?.Headless}, timeout: {ui?.ExplicitTimeoutSec}, download: {ui?.DownloadDirectory}, screenshots: {ui?.ScreenshotDirectory}");
            }

            return ui ?? throw new ArgumentException(nameof(UI));
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