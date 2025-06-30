using OpenQA.Selenium;

namespace WebApp.Pages;

public abstract class PageObjectBase
{
    protected PageObjectBase(IWebDriver? driver)
       : this(driver, TimeSpan.FromSeconds(5))
    {
    }

    protected PageObjectBase(IWebDriver? driver, TimeSpan timeout)
    {
        Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(timeout, TimeSpan.Zero);
        WaitHelp = new WaitHelper(Driver, timeout);
    }

    protected IWebDriver Driver { get; init; }

    protected WaitHelper WaitHelp { get; init; }
}
