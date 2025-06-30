using OpenQA.Selenium;

namespace WebApp.Pages;

public class JobListings : PageBase
{
    private readonly By sortByDate = By.CssSelector("#sort-time ~ label");
    private readonly By lastApplyButton = By.XPath("//ul[@class='search-result__list']/li[last()]//a[contains(text(),'apply')]");

    internal JobListings(IWebDriver driver)
        : base(driver)
    {
    }

    public JobListings SortResultByDate()
    {
        WaitHelp.ClickOnElement(sortByDate);
        return this;
    }

    public JobDetail ClicOnTheLastApplyButton()
    {
        WaitHelp.ClickOnElement(lastApplyButton);
        return new JobDetail(Driver);
    }
}
