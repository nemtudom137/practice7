using System.Diagnostics;
using System.Text.RegularExpressions;
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
       "RPA"
    ];

    private readonly string url = "https://www.epam.com/";
    private IWebDriver driver;
    private WebDriverWait explicitWait;

    [SetUp]
    public void Setup()
    {
        Trace.Listeners.Add(new ConsoleTraceListener());
        this.driver = new ChromeDriver();

        this.explicitWait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10))
        {
            PollingInterval = TimeSpan.FromMilliseconds(500),
        };

        this.explicitWait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException), typeof(ElementNotInteractableException), typeof(StaleElementReferenceException));

        this.driver.Manage().Window.Maximize();
        this.driver.Navigate().GoToUrl(this.url);
        this.AcceptCookies();
    }

    [TearDown]
    public void TearDown()
    {
        this.driver.Quit();
        this.driver?.Dispose();
        Trace.Flush();
    }

    [Test]
    [TestCaseSource(nameof(CarrierSearch))]
    public void CarrierSearchResult_ContainsLanguage(string language, string location, string city)
    {
        this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        var carriersLink = this.driver.FindElement(By.LinkText("Careers"));
        carriersLink.Click();

        var searchForm = this.driver.FindElement(By.Id("jobSearchFilterForm"));

        var keyword = searchForm.FindElement(By.Id("new_form_job_search-keyword"));
        keyword.SendKeys(language);

        var locations = searchForm.FindElement(By.ClassName("recruiting-search__location"));
        locations.Click();

        if (string.IsNullOrEmpty(city))
        {
            locations.FindElement(By.XPath($"//li[contains(text(),'{location}')]")).Click();
        }
        else
        {
            locations.FindElement(By.XPath($"//strong[contains(text(),'{location}')]")).Click();
            locations.FindElement(By.XPath($"//li[contains(text(),'{city}')]")).Click();
        }

        var remoteOption = searchForm.FindElement(By.Name("remote"));

        new Actions(this.driver)
            .MoveToElement(remoteOption)
            .Click()
            .Perform();

        var findButton = searchForm.FindElement(By.TagName("button"));
        findButton.Click();

        var sortByDate = this.driver.FindElement(By.CssSelector("#sort-time ~ label"));
        sortByDate.Click();

        var applyButtons = this.driver.FindElements(By.LinkText("VIEW AND APPLY"));
        applyButtons[0].Click();

        this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        var result = this.explicitWait.Until(d =>
            d.FindElements(By.CssSelector("header h1, div.vacancy_content :is(h1, h2, h3, h4, p, li)"))
            .Any(x => Regex.IsMatch(x.Text, $"\\b{language}\\b", RegexOptions.IgnoreCase)));

        Assert.That(result, Is.True);
    }

    [Test]
    [TestCaseSource(nameof(GlobalSearch))]
    public void GlobalSearchResult_ContainsKeyword(string searchString)
    {
        var magnifierIcon = this.driver.FindElement(By.ClassName("search-icon"));
        magnifierIcon.Click();

        var searchPanel = this.WaitForDisplayElement(By.ClassName("header-search__panel"));
        var searchInput = this.WaitForDisplayElement(By.Id("new_form_search"));
        searchInput.SendKeys(searchString);

        var findButton = searchPanel.FindElement(By.XPath(".//*[@class='search-results__input-holder']/following-sibling::button"));
        findButton.Click();

        var result = this.GetAllSearchResults().All(x => x.Contains(searchString, StringComparison.InvariantCultureIgnoreCase));

        Assert.That(result, Is.True);
    }

    private void AcceptCookies()
    {
        this.explicitWait.Until(driver =>
        {
            var acceptButton = driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            acceptButton.Click();
            return true;
        });
    }

    private IWebElement WaitForDisplayElement(By by)
    {
        return this.explicitWait.Until(d =>
        {
            var element = d.FindElement(by);
            if (element.Displayed)
            {
                return element;
            }

            return null;
        });
    }

    private void ScrollDownSearchResults(By resultElements)
    {
        var action = new Actions(this.driver);

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

            this.explicitWait.Until(d =>
            {
                var n = d.FindElements(resultElements).Count;
                if (n > currentCount)
                {
                    currentCount = n;
                    return false;
                }
                else
                {
                    return true;
                }
            });
        }
        while (totalCount > currentCount);
    }

    private List<string> GetAllSearchResults()
    {
        var action = new Actions(this.driver);

        var resultLocator = By.XPath("//article[@class='search-results__item']");
        var viewMoreButtonLocator = By.ClassName("search-results__view-more");

        while (true)
        {
            this.ScrollDownSearchResults(resultLocator);

            var button = this.explicitWait.Until(d => d.FindElement(viewMoreButtonLocator));
            try
            {
                action
                .ScrollToElement(button)
                .Pause(TimeSpan.FromSeconds(1))
                .MoveToElement(button)
                .Click()
                .Perform();
            }
            catch (ElementNotInteractableException)
            {
                break;
            }
        }

        var results = this.driver.FindElements(resultLocator).Select(x => x.Text).ToList();
        Debug.WriteLine(results.Count);
        return results;
    }
}