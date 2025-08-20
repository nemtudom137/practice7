using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Core.UI.DriverFactory;

internal class EdgeFactory : IDriverFactory
{
    public IWebDriver CreateDriver(bool headless, string downloadDirectory)
    {
        var options = new EdgeOptions();
        options.AddUserProfilePreference("download.default_directory", downloadDirectory);
        if (headless)
        {
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false);
            options.AddArguments("--disable-blink-features=AutomationControlled");
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36 Edg/123.0.0.0");
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("disable-infobars");

            IWebDriver driver = new EdgeDriver(options);
            ((IJavaScriptExecutor)driver).ExecuteScript("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})");

            return driver;
        }
        else
        {
            options.AddArgument("--start-maximized");
            return new EdgeDriver(options);
        }
    }
}
