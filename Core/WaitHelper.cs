using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Core;

public class WaitHelper
{
    public WaitHelper(IWebDriver driver, TimeSpan timeout)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(timeout, TimeSpan.Zero);
        Wait = new WebDriverWait(driver, timeout);
        Wait.IgnoreExceptionTypes(
            typeof(ElementClickInterceptedException),
            typeof(ElementNotInteractableException),
            typeof(StaleElementReferenceException));
    }

    public WebDriverWait Wait { get; init; }

    public IWebElement WaitForDisplayElement(By by) => Wait.Until(ExpectedConditions.ElementIsVisible(by));

    public void ClickOnElement(By by)
    {
        Wait.Until(d =>
        {
            d.FindElement(by).Click();
            return true;
        });
    }

    public void ClickOnElement(Func<string, By> getBy, string text)
    {
        Wait.Until(d =>
        {
            d.FindElement(getBy(text)).Click();
            return true;
        });
    }

    public ReadOnlyCollection<IWebElement> WaitForAnyElement(By by)
    {
        return Wait.Until(d =>
        {
            var elements = d.FindElements(by);
            if (elements.Count > 0)
            {
                return elements;
            }

            return null;
        });
    }

    public ReadOnlyCollection<IWebElement> WaitForAnyElement(By by, Predicate<IWebElement> predicate)
    {
        return Wait.Until(d =>
        {
            var elements = d.FindElements(by);
            if (elements.Count > 0 && elements.Any(x => predicate(x)))
            {
                return elements;
            }

            return null;
        });
    }

    public void WaitForDowloadedFile()
    {
        string directory = ConfigurationManager.Test.DownloadDirectory;
        Wait.Until(driver =>
        {
            var files = Directory.GetFiles(directory);
            return files is not null && files.Length != 0 &&
                !files.Any(f => f.EndsWith(".crdownload")) &&
                !files.Any(f => f.EndsWith(".part")) &&
                !files.Any(f => f.EndsWith(".tmp"));
        });
    }
}
