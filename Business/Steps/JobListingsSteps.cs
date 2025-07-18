using Business.ApplicationInterface;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Business.Steps;

[Binding]
public class JobListingsSteps
{
    private readonly JobListingsPage page;

    public JobListingsSteps(IWebDriver driver)
    {
        page = new JobListingsPage(driver);
    }

    [When(@"I sort the results by date")]
    public void WhenISortTheResultsByDate()
    {
        page.ClickWithWait(JobListingsPage.SortByDate);
    }

    [When(@"I click on the last Apply button")]
    public void WhenIClickOnTheLastApplyButton()
    {
        page.ClickWithWait(JobListingsPage.LastApplyButton);
    }
}
