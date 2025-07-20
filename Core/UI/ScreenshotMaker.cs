using System.Drawing.Imaging;
using NLog;
using OpenQA.Selenium;

namespace Core.UI;

public static class ScreenshotMaker
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    private static readonly ImageFormat ImageFormat = ImageFormat.Png;

    public static void TakeBrowserScreenshot()
    {
        try
        {
            var driver = DriverContainer.GetDriver();
            var fileName = $"{TestInfoHelper.GetTestName()}_{DateTime.Now:yyyy-MM-dd_hh-mm-ss-fff}.{ImageFormat}";
            var path = Path.Combine(ConfigurationManager.UI.ScreenshotDirectory, fileName);

            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(path);
            Log.Info($"Screenshot saved: {path}");
        }
        catch (Exception ex)
        {
            Log.Error($"Failed to take screenshot: {ex.Message}", ex);
        }
    }
}