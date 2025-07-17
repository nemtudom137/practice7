using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Core.DriverFactory;

internal class FirefoxFactory : IDriverFactory
{
    public IWebDriver CreateDriver(bool headless, string downloadDirectory)
    {
        var options = new FirefoxOptions
        {
            Profile = new FirefoxProfile(),
        };

        options.Profile.SetPreference("browser.download.folderList", 2);
        options.Profile.SetPreference("browser.download.dir", downloadDirectory);

        if (headless)
        {
            options.AddArgument("-headless");
            options.AddArgument("--width=1920");
            options.AddArgument("--height=1080");
            options.Profile.SetPreference(
                "general.useragent.override",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

            return new FirefoxDriver(options);
        }

        var driver = new FirefoxDriver(options);
        driver.Manage().Window.Maximize();

        return driver;
    }
}
