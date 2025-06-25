using OpenQA.Selenium;

namespace WebApp.Pages;

public class HomePage : BasePage
{

    private readonly string url = "https://www.epam.com/";

    public HomePage(IWebDriver driver) : base(driver, TimeSpan.FromSeconds(5))
    {
        Driver.Navigate().GoToUrl(url);
    }

    public new HomePage AcceptCookies()
    {
        base.AcceptCookies();
        return this;
    }
}
