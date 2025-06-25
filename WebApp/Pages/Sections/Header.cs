using OpenQA.Selenium;

namespace WebApp.Pages.Sections;

public class Header : PageObjectBase
{
    private readonly By carriers = By.XPath("//header//a[text()='Careers']");
    private readonly By about = By.XPath("//header//a[text()='About']");
    private readonly By insights = By.XPath("//header//a[text()='Insights']");
    private readonly By searchIcon = By.XPath("//button[contains(@class,'header-search')]");
    private readonly By searchInput = By.CssSelector("input#new_form_search");
    private readonly By findButton = By.XPath("//div[contains(@class,'search-results__input-holder')]/following-sibling::button");

    internal Header(IWebDriver driver) : base(driver)
    {
    }

    public Header ClickOnSearchIcon()
    {
        Driver.FindElement(searchIcon).Click();
        return this;
    }

    public CareersPage ClickOnCareers()
    {
        Driver.FindElement(carriers).Click();
        return new CareersPage(Driver);
    }

    public AboutPage ClickOnAbout()
    {
        Driver.FindElement(about).Click();
        return new AboutPage(Driver);
    }

    public InsightsPage ClickOnInsights()
    {
        Driver.FindElement(insights).Click();
        return new InsightsPage(Driver);
    }

    public Header SetSearchTerms(string searchTerms)
    {
        var searchField = WaitHelp.WaitForDisplayElement(searchInput);
        searchField.SendKeys(searchTerms);
        return this;
    }

    public SearchPage ClickOnFind()
    {
        Driver.FindElement(findButton).Click();
        return new SearchPage(Driver);
    }
}
