using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace WebDriverTask;

public class SimpleTests
{
    private static readonly object[][] CarrierSearch =
    [
        ["C", "All Locations", string.Empty],
        ["Java", "Japan", "Tokyo"]
    ];

    private static readonly object[] GlobalSearch =
    [
       "BLOCKCHAIN",
       "Cloud"
    ];

    private readonly string url = "https://www.epam.com/";
    private IWebDriver driver;
    private WebDriverWait explicitWait;

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();

        explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
        {
            PollingInterval = TimeSpan.FromMilliseconds(500),
        };

        explicitWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException), typeof(ElementNotInteractableException), typeof(StaleElementReferenceException));

        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl(url);
        ClickOnElement(By.XPath("//button[text()='Accept All']"));
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver?.Dispose();
    }

    [Test]
    [TestCaseSource(nameof(CarrierSearch))]
    public void CarrierSearchResult_ContainsLanguage(string language, string location, string city)
    {
        var carriersLink = driver.FindElement(By.LinkText("Careers"));
        carriersLink.Click();

        var keyword = WaitForDisplayElement(By.CssSelector("input#new_form_job_search-keyword"));
        keyword.SendKeys(language);

        var locations = driver.FindElement(By.CssSelector(".recruiting-search__location span[role='combobox']"));
        locations.Click();

        if (string.IsNullOrEmpty(city))
        {
            ClickOnElement(By.XPath($"//li[contains(text(),'{location}')]"));
        }
        else
        {
            ClickOnElement(By.XPath($"//strong[contains(text(),'{location}')]"));
            ClickOnElement(By.XPath($"//li[contains(text(),'{city}')]"));
        }

        var remoteOption = driver.FindElement(By.CssSelector(".job-search__filter-list input[name='remote']"));

        new Actions(driver)
            .MoveToElement(remoteOption)
            .Click()
            .Perform();

        var findButton = driver.FindElement(By.CssSelector("button[type='submit']"));
        findButton.Click();

        var sortByDate = WaitForDisplayElement(By.CssSelector("#sort-time ~ label"));
        sortByDate.Click();

        var applyButton = WaitForDisplayElement(By.XPath("//ul[@class='search-result__list']/li[last()]//a[contains(text(),'apply')]"));
        applyButton.Click();

        int result;

        try
        {
            result = WaitForAllElements(By.XPath($"//article//*[contains(text(),'{language}')]")).Count;
        }
        catch (WebDriverTimeoutException)
        {
            result = 0;
        }

        Assert.That(result, Is.GreaterThan(0));
    }

    [Test]
    [TestCaseSource(nameof(GlobalSearch))]
    public void GlobalSearchFirstResults_ContainKeyword(string searchString)
    {
        var magnifierIcon = driver.FindElement(By.XPath("//button[contains(@class,'header-search')]"));
        magnifierIcon.Click();

        var searchInput = WaitForDisplayElement(By.CssSelector("input#new_form_search"));
        searchInput.SendKeys(searchString);

        var findButton = driver.FindElement(By.XPath("//div[contains(@class,'search-results__input-holder')]/following-sibling::button"));
        findButton.Click();

        var results = WaitForAllElements(By.XPath("//article[@class='search-results__item']")).Select(x => x.Text);

        Assert.That(results.All(x => x.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)), Is.True);
    }

    private void ClickOnElement(By by)
    {
        explicitWait.Until(d =>
        {
            var acceptButton = d.FindElement(by);
            acceptButton.Click();
            return true;
        });
    }

    private IWebElement WaitForDisplayElement(By by)
    {
        return explicitWait.Until(d =>
        {
            var element = d.FindElement(by);
            if (element != null && element.Displayed && element.Enabled)
            {
                return element;
            }

            return null;
        });
    }

    private ReadOnlyCollection<IWebElement> WaitForAllElements(By by)
    {
        return explicitWait.Until(d =>
        {
            var elements = d.FindElements(by);
            if (elements.Count > 0 && elements.All(x => x.Enabled))
            {
                return elements;
            }

            return null;
        });
    }
}