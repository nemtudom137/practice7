
using OpenQA.Selenium;

namespace Core.DriverFactory;

internal static class DriverCreator
{
    public static IWebDriver CreateDriver()
    {
        var browser = ConfigurationManager.Test.Browser;
        IDriverFactory factory = browser switch
        {
            BrowserType.Chrome => new ChromeFactory(),
            BrowserType.Firefox => new FirefoxFactory(),
            BrowserType.Edge => new EdgeFactory(),
            _ => throw new NotSupportedException("Not supported browser"),
        };

        var driver = factory.CreateDriver(ConfigurationManager.Test.Headless, ConfigurationManager.Test.DirectoryForDownload);
        LogHelper.Info("Driver is created.");
        return driver;
    }
}