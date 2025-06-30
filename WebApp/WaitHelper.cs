using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebApp;
public class WaitHelper
{
    public WaitHelper(IWebDriver driver, TimeSpan timeout)
    {
        Wait = new WebDriverWait(driver, timeout);
        Wait.IgnoreExceptionTypes(
            typeof(ElementClickInterceptedException),
            typeof(ElementNotInteractableException),
            typeof(StaleElementReferenceException));
    }

    public WebDriverWait Wait { get; init; }

    public IWebElement WaitForDisplayElement(By by)
    {
        return Wait.Until(d =>
        {
            var element = d.FindElement(by);
            if (element != null && element.Displayed && element.Enabled)
            {
                return element;
            }

            return null;
        });
    }

    public void ClickOnElement(By by)
    {
        Wait.Until(d =>
        {
            d.FindElement(by).Click();
            return true;
        });
    }

    public ReadOnlyCollection<IWebElement> WaitForAllElements(By by)
    {
        return Wait.Until(d =>
        {
            var elements = d.FindElements(by);
            if (elements.Count > 0 && elements.All(x => x.Enabled))
            {
                return elements;
            }

            return null;
        });
    }

    public bool AnyElementDisplayed(By by, Predicate<IWebElement> predicate)
    {
        return Wait.Until(d =>
        {
            var elements = d.FindElements(by);
            if (elements.Count > 0 && elements.Any(x => x.Displayed && predicate(x)))
            {
                return true;
            }

            return false;
        });
    }

    public void WaitForDowloadedFile(string directory)
    {
        Wait.Until(driver =>
        {
            var files = Directory.GetFiles(directory);
            return files is not null && files.Length != 0 &&
                !files.Any(f => f.EndsWith(".crdownload")) &&
                !files.Any(f => f.EndsWith(".part"));
        });
    }
}