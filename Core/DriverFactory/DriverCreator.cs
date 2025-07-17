using NLog;
using OpenQA.Selenium;

namespace Core.DriverFactory;

public static class DriverCreator
{
    public static readonly Logger Log = LogManager.GetCurrentClassLogger();

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

        var driver = factory.CreateDriver(ConfigurationManager.Test.Headless, ConfigurationManager.Test.DownloadDirectory);
        Log.Info("Driver is created.");
        return driver;
    }
}