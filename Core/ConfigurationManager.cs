using Core.API;
using Core.UI;
using Microsoft.Extensions.Configuration;
using System;

namespace Core;

public static class ConfigurationManager
{
    private static readonly string UISection = "UI";
    private static readonly string APISection = "API";
    private static IConfigurationRoot config = GetConfiguration();
    private static UiTestConfiguration? ui;
    private static APITestConfiguration? api;

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

    public static ApiTestConfiguration API
    {
        get
        {
            if (api is null)
            {
                ui = config.GetSection(APISection).Get<UiTestConfiguration>();
                LogHelper.Log.Info($"Config: {api?.Url}");
            }
            
            return api ?? throw new ArgumentException(nameof(API));
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