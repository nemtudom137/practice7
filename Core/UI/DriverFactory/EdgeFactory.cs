using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            options.AddArguments(new List<string>()
            {
                "--headless=new",
                "--window-size=1920,1080",
                "user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36",
                "--disable-blink-features=AutomationControlled",
                "--disable-gpu",
                "--no-sandbox",
                "--disable-dev-shm-usage",
                "--disable-web-security",
                "--disable-features=VizDisplayCompositor",
                "--disable-notifications",
                "--disable-popup-blocking",
                "--window-size=1920,1080",
                "--disable-infobars",
                "--disable-blink-features=AutomationControlled",
                "user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36",
                "--disable-logging",
                "--disable-dev-tools",
                "--no-first-run",
                "--no-default-browser-check",
                "--disable-background-timer-throttling",
                "--disable-backgrounding-occluded-windows",
                "--disable-renderer-backgrounding",
                "--disable-features=TranslateUI",
                "--disable-ipc-flooding-protection",
                "--password-store=basic",
                "--use-mock-keychain",
                "--disable-client-side-phishing-detection",
                "--disable-sync",
                "--disable-features=UserAgentClientHint",
                "--disable-blink-features",
                "--disable-component-extensions-with-background-pages",
            });

            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false);
            options.AddUserProfilePreference("prefs", new Dictionary<string, object>
            {
                ["credentials_enable_service"] = false,
                ["profile.password_manager_enabled"] = false,
            });

            EdgeDriverService service = EdgeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            IWebDriver driver = new EdgeDriver(service, options);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})");
            js.ExecuteScript("window.chrome = { runtime: {} }");
            js.ExecuteScript(@"
                const originalQuery = window.navigator.permissions.query;
                window.navigator.permissions.query = (parameters) => (
                    parameters.name === 'notifications' ?
                        Promise.resolve({ state: Notification.permission }) :
                        originalQuery(parameters)
                );
            ");

            return driver;
        }
        else
        {
            options.AddArgument("--start-maximized");
            return new EdgeDriver(options);
        }
    }
}
