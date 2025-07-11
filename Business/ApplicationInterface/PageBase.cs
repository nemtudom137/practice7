using Core;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Business.ApplicationInterface;

public abstract class PageBase
{
    protected PageBase()
    {
        WaitHelper = new WaitHelper();
    }

    protected PageBase(TimeSpan timeout)
    {
        WaitHelper = new WaitHelper(timeout);
    }

    public WaitHelper WaitHelper { get; init; }

    public static void Click(By by)
    {
        DriverContainer.Driver.FindElement(by).Click();
    }

    public static void ScrollToElement(By by)
    {
        var element = DriverContainer.Driver.FindElement(by);
        new Actions(DriverContainer.Driver).ScrollToElement(element).Perform();
    }

    public void ClickWithWait(By by) => WaitHelper.ClickOnElement(by);

    public void SetField(By by, string input) => WaitHelper.WaitForDisplayElement(by).SendKeys(input);
}