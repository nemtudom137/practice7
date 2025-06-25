using OpenQA.Selenium;
using WebApp.Pages.Sections;

namespace WebApp.Pages;

public abstract class BasePage : PageObjectBase
{    
    protected BasePage(IWebDriver driver) : this(driver, TimeSpan.FromSeconds(2))
    {
    }

    protected BasePage(IWebDriver driver, TimeSpan timeout) : base(driver, timeout)
    {
        Header = new Header(driver);
    }

    public Header Header { get; init; }    
}
