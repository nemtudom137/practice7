using Core.API;
using Core.UI;
using Microsoft.Extensions.Configuration;

namespace Core;

public static class ConfigurationManager
{
    private static readonly string UISection = "UI";
    private static readonly string APISection = "API";
    private static IConfigurationRoot config = GetConfiguration();
    private static UiTestConfiguration? ui;
    private static ApiTestConfiguration? api;

    public static UiTestConfiguration UI
    {
        get
        {
            if (ui is null)
            {
                ui = config.GetSection(UISection).Get<UiTestConfiguration>() ?? throw new ArgumentException(nameof(UI));
                if (Enum.TryParse<BrowserType>(Environment.GetEnvironmentVariable("BROWSER"), out BrowserType browser))
                {
                    ui.Browser = browser;
                    ui.Headless = true;
                }

                LogHelper.Log.Info($"Config: {ui.Url}, {ui.Browser}, headless: {ui.Headless}, timeout: {ui.ExplicitTimeoutSec}, download: {ui.DownloadDirectory}, screenshots: {ui.ScreenshotDirectory}");
            }

            return ui;
        }
    }

    public static ApiTestConfiguration API
    {
        get
        {
            if (api is null)
            {
                api = config.GetSection(APISection).Get<ApiTestConfiguration>() ?? throw new ArgumentException(nameof(API));
                LogHelper.Log.Info($"Config: {api.Url}");
            }

            return api;
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