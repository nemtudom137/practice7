using OpenQA.Selenium;

namespace Core.DriverFactory;

internal static class DriverCreator
{
    public static IWebDriver CreateDriver()
    {
        var browser = ConfigurationReader.Test.Browser;
        IDriverFactory factory = browser switch
        {
            BrowserType.Chrome => new ChromeFactory(),
            BrowserType.Firefox => new FirefoxFactory(),
            BrowserType.Edge => new EdgeFactory(),
            _ => throw new NotSupportedException("Not supported browser"),
        };

        return factory.CreateDriver(ConfigurationReader.Test.Headless, ConfigurationReader.Test.TestDirectory);
    }
}