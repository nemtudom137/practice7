using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core.UI.DriverFactory;

internal class ChromeFactory : IDriverFactory
{
    private static readonly string[] UserAgents =
    [
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Edg/116.0.1938.62 Safari/537.36",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/115.0.0.0 Edg/115.0.1901.183 Safari/537.36",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Edg/114.0.1823.82 Safari/537.36",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Edg/113.0.1774.50 Safari/537.36",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Edg/112.0.1722.68 Safari/537.36",
        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Edg/111.0.1661.43 Safari/537.36",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.0.0 Edg/110.0.1587.49 Safari/537.36",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Edg/109.0.1518.61 Safari/537.36",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Edg/109.0.1518.61 Safari/537.36"
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
            options.AddArgument("disable-infobars");
            options.AddArgument("--disable-blink-features=AutomationControlled");
            var agent = UserAgents[new Random().Next(UserAgents.Length)];
            options.AddArgument($"user-agent={agent}");
        }
        else
        {
            options.AddArgument("--start-maximized");
        }

        IWebDriver driver = new ChromeDriver(options);
        ((IJavaScriptExecutor)driver).ExecuteScript("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})");

        return driver;
    }
}
