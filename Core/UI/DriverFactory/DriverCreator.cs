using NLog;
using OpenQA.Selenium;

namespace Core.UI.DriverFactory;

public static class DriverCreator
{
    public static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public static IWebDriver CreateDriver()
    {
        var browser = ConfigurationManager.UI.Browser;
        IDriverFactory factory = browser switch
        {
            BrowserType.Chrome => new ChromeFactory(),
            BrowserType.Firefox => new FirefoxFactory(),
            BrowserType.Edge => new EdgeFactory(),
            _ => throw new NotSupportedException("Not supported browser"),
        };

        var driver = factory.CreateDriver(ConfigurationManager.UI.Headless, ConfigurationManager.UI.DownloadDirectory);
        Log.Info("Driver is created.");
        return driver;
    }
}