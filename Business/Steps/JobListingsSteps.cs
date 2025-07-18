using Business.ApplicationInterface;
using Core;
using NLog;
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
        LogHelper.Log.Info("Click on Sort By DATE.");
    }

    [When(@"I click on the last Apply button")]
    public void WhenIClickOnTheLastApplyButton()
    {
        page.ClickWithWait(JobListingsPage.LastApplyButton);
        LogHelper.Log.Info("Click on the last VIEW AND APPLY.");
    }
}
