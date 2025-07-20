using System.Drawing.Imaging;
using NLog;
using OpenQA.Selenium;

namespace Core.UI;

public class ScreenshotMaker
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    private static readonly ImageFormat ImageFormat = ImageFormat.Png;
    private readonly IWebDriver driver;

    public ScreenshotMaker(IWebDriver? driver)
    {
        this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
    }

    public void TakeBrowserScreenshot(string testName, params object?[] arguments)
    {
        try
        {
            var path = Path.Combine(ConfigurationManager.UI.ScreenshotDirectory, ScreenshotName(testName, arguments));
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(path);
            Log.Info($"Screenshot saved: {path}");
        }
        catch (Exception ex)
        {
            Log.Error($"Failed to take screenshot: {ex.Message}", ex);
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