using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumUndetectedChromeDriver;

namespace Core.UI.DriverFactory;

internal class ChromeFactory : IDriverFactory
{
    public IWebDriver CreateDriver(bool headless, string downloadDirectory)
    {
        var options = new ChromeOptions();
        options.AddUserProfilePreference("download.default_directory", downloadDirectory);

        if (headless)
        {
            options.AddArgument("--window-size=1920,1080");
            var driverPath = new ChromeDriverInstaller().Auto().Result;
            var driver = UndetectedChromeDriver.Create(driverExecutablePath: driverPath, options: options, headless: true);
            return driver;
        }
        else
        {
            options.AddArgument("--start-maximized");
            return new ChromeDriver(options);
        }
    }
}
