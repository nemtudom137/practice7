using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace WebApp.Pages;

public class CareersPage : PageBase
{
    private readonly By keyword = By.CssSelector("input#new_form_job_search-keyword");
    private readonly By locations = By.CssSelector(".recruiting-search__location span[role='combobox']");
    private readonly By remoteOption = By.CssSelector(".job-search__filter-list input[name='remote']");
    private readonly By findButton = By.CssSelector("button[type='submit']");

    internal CareersPage(IWebDriver driver)
        : base(driver)
    {
    }

    public CareersPage SetSearchTerms(string searchTerms)
    {
        Driver.FindElement(keyword).SendKeys(searchTerms);
        return this;
    }

    public CareersPage SetLocation(string location, string city)
    {
        Driver.FindElement(locations).Click();
        if (string.IsNullOrEmpty(city))
        {
            WaitHelp.ClickOnElement(GetLocationOption(location));
        }
        else
        {
            WaitHelp.ClickOnElement(GetLocationOptionGroup(location));
            WaitHelp.ClickOnElement(GetLocationOption(city));
        }

        return this;
    }

    public CareersPage ChooseRemote()
    {
        var remote = Driver.FindElement(remoteOption);
        new Actions(Driver).MoveToElement(remote)
           .Click()
           .Perform();

        return this;
    }

    public JobListings ClickOnFind()
    {
        Driver.FindElement(findButton).Click();
        return new JobListings(Driver);
    }

    private static By GetLocationOption(string location) => By.XPath($"//li[contains(text(),'{location}')]");

    private static By GetLocationOptionGroup(string location) => By.XPath($"//strong[contains(text(),'{location}')]");
}
