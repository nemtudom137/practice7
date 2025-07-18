using Core;
using OpenQA.Selenium;

namespace Business.ApplicationInterface;

public class HomePage : PageBase
{
    public static readonly By AcceptCookies = By.XPath("//button[text()='Accept All']");

    internal HomePage(IWebDriver driver)
        : base(driver)
    {
    }

    public void Open()
    {
        Driver.Navigate().GoToUrl(ConfigurationManager.UI.Url ?? throw new ArgumentException(nameof(ConfigurationManager.UI.Url)));
        LogHelper.Log.Trace($"Navigate to {ConfigurationManager.UI.Url}");
    }
}
