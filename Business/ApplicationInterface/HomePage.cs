using Core;
using OpenQA.Selenium;

namespace Business.ApplicationInterface;

public class HomePage : PageBase
{
    public static readonly By AcceptCookies = By.XPath("//button[text()='Accept All']");

    internal HomePage()
    {
        DriverContainer.Driver.Navigate().GoToUrl(ConfigurationReader.Test.Url);
    }
}
