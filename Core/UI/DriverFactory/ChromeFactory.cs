using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core.UI.DriverFactory;

internal class ChromeFactory : IDriverFactory
{
    public IWebDriver CreateDriver(bool headless, string downloadDirectory)
    {
        var options = new ChromeOptions();
        options.AddUserProfilePreference("download.default_directory", downloadDirectory);

        if (headless)
        {
            options.AddArguments(new List<string>()
            {
                "--headless=new",
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
            });

            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false);

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            IWebDriver driver = new ChromeDriver(service, options);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})");

            string script = @"
                    // Overwrite the navigator.plugins property
                    Object.defineProperty(navigator, 'plugins', {
                        get: () => [1, 2, 3, 4, 5]
                    });
                    
                    // Overwrite the navigator.languages property
                    Object.defineProperty(navigator, 'languages', {
                        get: () => ['en-US', 'en']
                    });
                    
                    // Overwrite the WebGL vendor and renderer
                    const getParameter = WebGLRenderingContext.prototype.getParameter;
                    WebGLRenderingContext.prototype.getParameter = function(parameter) {
                        if (parameter === 37445) {
                            return 'Intel Inc.';
                        }
                        if (parameter === 37446) {
                            return 'Intel Iris OpenGL Engine';
                        }
                        return getParameter(parameter);
                    };
                ";

            js.ExecuteScript(script);

            return driver;
        }
        else
        {
            options.AddArgument("--start-maximized");
            return new ChromeDriver(options);
        }
    }
}
