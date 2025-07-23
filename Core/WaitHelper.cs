using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Core;

public class WaitHelper
{
    private readonly WebDriverWait wait;

    public WaitHelper(IWebDriver driver, TimeSpan timeout)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(timeout, TimeSpan.Zero);
        wait = new WebDriverWait(driver, timeout);
        wait.IgnoreExceptionTypes(
            typeof(ElementClickInterceptedException),
            typeof(ElementNotInteractableException),
            typeof(StaleElementReferenceException));
    }

    public IWebElement WaitForElementToBeDispayed(By by) => wait.Until(ExpectedConditions.ElementIsVisible(by));

    public IWebElement WaitForElementToExist(By by) => wait.Until(ExpectedConditions.ElementExists(by));

    public void ClickOnElement(By by)
    {
        wait.Until(d =>
        {
            d.FindElement(by).Click();
            return true;
        });
    }

    public bool AnyElementDisplayed(By by, Predicate<IWebElement> predicate)
    {
        return wait.Until(d =>
        {
            var elements = d.FindElements(by);
            if (elements.Count > 0 && elements.Any(x => x.Displayed && predicate(x)))
            {
                return true;
            }

            return false;
        });
    }

    public void WaitForDowloadedFile()
    {
        string directory = ConfigurationManager.UI.DownloadDirectory;
        wait.Until(driver =>
        {
            var files = Directory.GetFiles(directory);
            return files is not null && files.Length != 0 &&
                !files.Any(f => f.EndsWith(".crdownload")) &&
                !files.Any(f => f.EndsWith(".part")) &&
                !files.Any(f => f.EndsWith(".tmp"));
        });
    }

    public int CountOfDynamicList(By item, int initalCount)
    {
        wait.Until(d =>
        {
            var n = d.FindElements(item).Count;
            if (n > initalCount)
            {
                initalCount = n;
                return false;
            }
            else
            {
                return true;
            }
        });

        return initalCount;
    }
}
