using Core;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Business.ApplicationInterface;

public abstract class PageBase
{
    public static readonly Logger Log = LogManager.GetCurrentClassLogger();

    protected PageBase(IWebDriver driver)
        : this(driver, TimeSpan.FromSeconds(ConfigurationManager.Test.ExplicitTimeoutSec))
    {
    }

    protected PageBase(IWebDriver driver, TimeSpan timeout)
    {
        Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(timeout, TimeSpan.Zero);
        WaitHelper = new WaitHelper(Driver, timeout);
    }

    public WaitHelper WaitHelper { get; init; }

    protected IWebDriver Driver { get; init; }

    public void Click(By by)
    {
        Driver.FindElement(by).Click();
        Log.Trace($"Element located by {by} is clicked");
    }

    public void ScrollToElement(By by)
    {
        var element = Driver.FindElement(by);
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        new Actions(Driver).ScrollToElement(element).Perform();
        Log.Trace($"Scrolled to element located by {by}");
    }

    public void ClickWithWait(By by)
    {
        WaitHelper.ClickOnElement(by);
        Log.Trace($"Element located by {by} is clicked");
    }

    public void SetField(By by, string input)
    {
        WaitHelper.WaitForDisplayElement(by).SendKeys(input);
        Log.Trace($"Field located by {by} is filed with {input}");
    }
}