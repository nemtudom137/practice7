using OpenQA.Selenium;
using WebApp.Pages.Sections;

namespace WebApp.Pages;

public abstract class PageBase : PageObjectBase
{
    protected PageBase(IWebDriver? driver)
        : base(driver)
    {
        Header = new Header(Driver);
    }

    protected PageBase(IWebDriver? driver, TimeSpan timeout)
        : base(driver, timeout)
    {
        Header = new Header(Driver);
    }

    public Header Header { get; init; }
}
