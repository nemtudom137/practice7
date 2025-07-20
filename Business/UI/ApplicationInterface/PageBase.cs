using Core;
using Core.UI;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Business.UI.ApplicationInterface;

public abstract class PageBase
{
    protected PageBase()
        : this(TimeSpan.FromSeconds(ConfigurationManager.UI.ExplicitTimeoutSec))
    {
    }

    protected PageBase(TimeSpan timeout)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(timeout, TimeSpan.Zero);
        WaitHelper = new WaitHelper(Driver, timeout);
    }

    public WaitHelper WaitHelper { get; init; }

    protected IWebDriver Driver => DriverContainer.GetDriver();

    public void Click(By by)
    {
        Driver.FindElement(by).Click();
        LogHelper.Log.Trace($"Element located by {by} is clicked");
    }

    public void ScrollToElement(By by)
    {
        var element = Driver.FindElement(by);
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        new Actions(Driver).ScrollToElement(element).Perform();
        LogHelper.Log.Trace($"Scrolled to element located by {by}");
    }

    public void ClickWithWait(By by)
    {
        WaitHelper.ClickOnElement(by);
        LogHelper.Log.Trace($"Element located by {by} is clicked");
    }

    public void SetField(By by, string input)
    {
        WaitHelper.WaitForDisplayElement(by).SendKeys(input);
        LogHelper.Log.Trace($"Field located by {by} is filed with {input}");
    }

    public void HoverOverElement(By by)
    {
        var element = Driver.FindElement(by);
        new Actions(Driver).MoveToElement(element).Perform();
        LogHelper.Log.Trace($"Hovering over element located by {by}");
    }
}