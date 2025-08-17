using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core.UI.DriverFactory;

internal class ChromeFactory : IDriverFactory
{
    private static readonly string[] UserAgents =
    [
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:135.0) Gecko/20100101 Firefox/135.0",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/127.0.0.0 Safari/537.36",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/117.0.0.0 Safari/537.36"
     ];

    public IWebDriver CreateDriver(bool headless, string downloadDirectory)
    {
        var options = new ChromeOptions();
        options.AddUserProfilePreference("download.default_directory", downloadDirectory);

        if (headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--auto-open-devtools-for-tabs");
            options.AddArgument("disable-infobars");
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false);
            var agent = UserAgents[new Random().Next(UserAgents.Length)];
            options.AddArgument($"--user-agent={agent}");
            options.AddExcludedArguments(new List<string>() { "enable-automation" });

            var driver = new ChromeDriver(options);
            ((IJavaScriptExecutor)driver).ExecuteScript("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})");

            return driver;
        }
        else
        {
            options.AddArgument("--start-maximized");
            return new ChromeDriver(options);
        }
    }
}
