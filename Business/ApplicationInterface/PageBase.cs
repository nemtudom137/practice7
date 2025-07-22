using Core;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Business.ApplicationInterface;

public abstract class PageBase
{
    public static readonly Logger Log = LogManager.GetCurrentClassLogger();

    protected PageBase(IWebDriver driver)
        : this(driver, TimeSpan.FromSeconds(ConfigurationManager.UI.ExplicitTimeoutSec))
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
        WaitHelper.ClickOnElement(by);
        Log.Trace($"Element located by {by} is clicked");
    }

    public void ClickOnDynamicElement(By element)
    {
        var button = WaitHelper.WaitForElementToExist(element);

        new Actions(Driver).ScrollToElement(button)
        .Pause(TimeSpan.FromSeconds(1))
        .MoveToElement(button)
        .Click()
        .Perform();
    }

    public void ChooseOption(By by)
    {
        var remote = Driver.FindElement(by);
        new Actions(Driver).MoveToElement(remote)
           .Click()
           .Perform();

        Log.Trace($"Element located by {remote} is clicked");
    }

    public void SetField(By by, string input)
    {
        WaitHelper.WaitForElementToBeDispayed(by).SendKeys(input);
        Log.Trace($"Field located by {by} is filed with {input}");
    }

    public void ScrollToElement(By by)
    {
        var element = Driver.FindElement(by);
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        new Actions(Driver).ScrollToElement(element).Perform();
        Log.Trace($"Scrolled to element located by {by}");
    }

    public void ScrollDownDynamicElements(By item)
    {
        var action = new Actions(Driver);

        int totalCount = 0;
        int currentCount = 0;

        do
        {
            totalCount = currentCount;

            action.Pause(TimeSpan.FromSeconds(1))
                .KeyDown(Keys.Control)
                .SendKeys(Keys.End)
                .KeyUp(Keys.Control)
                .Perform();

            Log.Trace($"Scroll to the bottom.");

            currentCount = WaitHelper.CountOfDynamicList(item, currentCount);
        }
        while (totalCount > currentCount);
    }

    public bool IsTextPresentInElement(By by, string text)
    {
        return WaitHelper.AnyElementDisplayed(by, x => x.Text.Contains(text));
    }

    public string GetElementText(By by)
    {
        return WaitHelper.WaitForElementToBeDispayed(by).Text;
    }
}