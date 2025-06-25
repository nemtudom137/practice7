using OpenQA.Selenium;
using WebApp.Pages.Sections;

namespace WebApp.Pages;

public abstract class BasePage : PageObjectBase
{
    private readonly By acceptCookies = By.XPath("//button[text()='Accept All']");

    protected BasePage(IWebDriver driver) : this(driver, TimeSpan.FromSeconds(2))
    {
    }

    protected BasePage(IWebDriver driver, TimeSpan timeout) : base(driver, timeout)
    {
        Header = new Header(driver);
    }

    public Header Header { get; init; }

    protected virtual void AcceptCookies()
    {
        Wait.ClickOnElement(acceptCookies);
    }
}
