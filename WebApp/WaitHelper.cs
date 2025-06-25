using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace WebApp;
public class WaitHelper
{
    private readonly IWebDriver driver;
    private readonly TimeSpan timeout;

    public WaitHelper(IWebDriver driver, TimeSpan timeout)
    {
        this.driver = driver;
        this.timeout = timeout;
    }

    public IWebElement WaitForDisplayElement(By by)
    {
        return new WebDriverWait(driver, timeout).Until(d =>
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
        var wait = new WebDriverWait(driver, timeout)
        {
            PollingInterval = TimeSpan.FromMilliseconds(500),
        };

        wait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException), typeof(ElementNotInteractableException), typeof(StaleElementReferenceException));
        wait.Until(d =>
        {
            var element = d.FindElement(by);
            element.Click();
            return true;
        });
    }

    public ReadOnlyCollection<IWebElement> WaitForAllElements(By by)
    {
        var wait = new WebDriverWait(driver, timeout)
        {
            PollingInterval = TimeSpan.FromMilliseconds(500),
        };

        wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
        return wait.Until(d =>
        {
            var elements = d.FindElements(by);
            if (elements.Count > 0 && elements.All(x => x.Enabled))
            {
                return elements;
            }

            return null;
        });
    }

    public bool AnyElementWith(By by, Predicate<IWebElement> predicate)
    {
        var wait = new WebDriverWait(driver, timeout)
        {
            PollingInterval = TimeSpan.FromMilliseconds(500),
        };

        wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
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

    public void WaitForDowloadedFile(string directory)
    {
        var wait = new WebDriverWait(driver, timeout);
        wait.Until(driver =>
        {
            var files = Directory.GetFiles(directory);
            return files is not null && files.Length != 0 &&
                !files.Any(f => f.EndsWith(".crdownload")) &&
                !files.Any(f => f.EndsWith(".part"));
        });
    }
}