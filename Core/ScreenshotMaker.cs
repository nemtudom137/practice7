using OpenQA.Selenium;

namespace Core;

public static class ScreenshotMaker
{
    public static void TakeBrowserScreenshot()
    {
        var now = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff");
        var screenshotPath = Path.Combine(Environment.CurrentDirectory, $"Display_{now}.png");
        Screenshot screenshot = ((ITakesScreenshot)DriverContainer.Driver).GetScreenshot();
        screenshot.SaveAsFile(screenshotPath);
    }
}