using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Business.UI.ApplicationInterface;

public abstract class PageBase
{
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

    protected WaitHelper WaitHelper { get; init; }

    protected IWebDriver Driver { get; init; }

    public void Click(By by)
    {
        WaitHelper.ClickOnElement(by);
        LogHelper.Log.Trace($"Element located by {by} is clicked");
    }

    public void ClickOnDynamicElement(By element)
    {
        var button = WaitHelper.WaitForElementToExist(element);

        new Actions(Driver).ScrollToElement(button)
        .Pause(TimeSpan.FromSeconds(1))
        .MoveToElement(button)
        .Click()
        .Perform();
        LogHelper.Log.Trace($"Element located by {element} is clicked");
    }

    public void ChooseOption(By by)
    {
        var remote = Driver.FindElement(by);
        new Actions(Driver).MoveToElement(remote)
           .Click()
           .Perform();

        LogHelper.Log.Trace($"Element located by {remote} is clicked");
    }

    public void SetField(By by, string input)
    {
        WaitHelper.WaitForElementToBeDispayed(by).SendKeys(input);
        LogHelper.Log.Trace($"Field located by {by} is filed with {input}");
    }

    public void ScrollToElement(By by)
    {
        var element = Driver.FindElement(by);
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        new Actions(Driver).ScrollToElement(element).Perform();
        LogHelper.Log.Trace($"Scrolled to element located by {by}");
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

            LogHelper.Log.Trace($"Scroll to the bottom to see all element locatetd by {item}.");

            currentCount = WaitHelper.CountOfDynamicList(item, currentCount);
        }
        while (totalCount > currentCount);
    }

    public bool IsTextPresentInElement(By by, string text)
    {
        var result = WaitHelper.AnyElementDisplayed(by, x => x.Text.Contains(text));
        LogHelper.Log.Trace($"'{text}' {(result ? "is" : "is not")} present in the text of element located by {by} is clicked");

        return result;
    }

    public bool IsElementPresent(By by)
    {
        try
        {
            WaitHelper.WaitForElementToExist(by);
            return true;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public string GetElementText(By by)
    {
        var text = WaitHelper.WaitForElementToBeDispayed(by).Text;
        LogHelper.Log.Trace($"Text of the element located by {by} is '{text}'");

        return text;
    }

    public void HoverOverElement(By by)
    {
        var element = Driver.FindElement(by);
        new Actions(Driver).MoveToElement(element).Perform();
        LogHelper.Log.Trace($"Hovering over element located by {by}");
    }

    public void WaitForDowloadedFile() => WaitHelper.WaitForDowloadedFile();
}