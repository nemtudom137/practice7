using Core;
using OpenQA.Selenium;

namespace Business.ApplicationInterface;

public class HomePage : PageBase
{
    public static readonly By AcceptCookies = By.XPath("//button[text()='Accept All']");

    internal HomePage(IWebDriver driver)
        : base(driver)
    {
        Driver.Navigate().GoToUrl(ConfigurationManager.Test.Url);
        Log.Trace($"Navigate to {ConfigurationManager.Test.Url}");
    }
}
