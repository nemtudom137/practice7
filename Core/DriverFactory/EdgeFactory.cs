using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Core.DriverFactory;

internal class EdgeFactory : IDriverFactory
{
    public IWebDriver CreateDriver(bool headless, string downloadDirectory)
    {
        var options = new EdgeOptions();
        options.AddUserProfilePreference("download.default_directory", downloadDirectory);
        if (headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
        }
        else
        {
            options.AddArgument("--start-maximized");
        }

        return new EdgeDriver(options);
    }
}
