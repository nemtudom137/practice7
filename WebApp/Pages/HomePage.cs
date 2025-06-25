using OpenQA.Selenium;

namespace WebApp.Pages;

public class HomePage : BasePage
{

    private readonly string url = "https://www.epam.com/";
    private readonly By acceptCookies = By.XPath("//button[text()='Accept All']");

    public HomePage(IWebDriver driver) : base(driver, TimeSpan.FromSeconds(5))
    {
        Driver.Navigate().GoToUrl(url);
    }

    public HomePage AcceptCookies()
    {
        WaitHelp.ClickOnElement(acceptCookies);
        return this;
    }
}
