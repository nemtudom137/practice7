using NLog;
using OpenQA.Selenium;

namespace Business.Business;

public abstract class ContextBase
{
    protected ContextBase(IWebDriver? driver)
    {
        Driver = driver ?? throw new ArgumentNullException(nameof(driver));
    }

    protected IWebDriver Driver { get; init; }
}
