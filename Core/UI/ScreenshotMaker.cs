using System.Drawing.Imaging;
using NLog;
using OpenQA.Selenium;

namespace Core.UI;

public class ScreenshotMaker
{
    private static readonly string ImageFormat = "png";
    private readonly IWebDriver driver;

    public ScreenshotMaker(IWebDriver? driver)
    {
        this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
    }

    public void TakeBrowserScreenshot()
    {
        try
        {
            var fileName = $"{TestInfoHelper.GetTestName()}_{DateTime.Now:yyyy-MM-dd_hh-mm-ss-fff}.{ImageFormat}";
            var path = Path.Combine(ConfigurationManager.UI.ScreenshotDirectory, fileName);

            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(path);
            LogHelper.Log.Info($"Screenshot saved: {path}");
        }
        catch (Exception ex)
        {
            LogHelper.Log.Error($"Failed to take screenshot: {ex.Message}", ex);
        }
    }
}