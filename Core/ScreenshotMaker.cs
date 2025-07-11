using System.Drawing.Imaging;
using OpenQA.Selenium;

namespace Core;

public static class ScreenshotMaker
{
    private static readonly ImageFormat ImageFormat = ImageFormat.Png;

    public static void TakeBrowserScreenshot(string testName, params object?[] arguments)
    {
        try
        {
            var path = Path.Combine(ConfigurationManager.Test.DirectoryForScreenshots, ScreenshotName(testName, arguments));
            Screenshot screenshot = ((ITakesScreenshot)DriverContainer.Driver).GetScreenshot();
            screenshot.SaveAsFile(path);
            LogHelper.Info($"Screenshot saved: {path}");
        }
        catch (Exception ex)
        {
            LogHelper.Error($"Failed to take screenshot: {ex.Message}");
        }
    }

    private static string ScreenshotName(string testName, params object?[] arguments)
    {
        string fileName;
        var time = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff");
        if (arguments is null || !arguments.Any(x => x is not null))
        {
            fileName = $"{testName}_{time}.{ImageFormat}";
        }
        else
        {
            string argString = string.Join("-", arguments);
            fileName = $"{testName}_{argString}_{time}.{ImageFormat}";
        }

        return fileName;
    }
}