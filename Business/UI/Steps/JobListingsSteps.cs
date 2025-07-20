using Business.UI.ApplicationInterface;
using Core;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Business.UI.Steps;

[Binding]
public class JobListingsSteps
{
    private readonly JobListingsPage page;

    public JobListingsSteps()
    {
        page = new JobListingsPage();
    }

    [When(@"I sort the results by date")]
    public void WhenISortTheResultsByDate()
    {
        page.Click(JobListingsPage.SortByDate);
        LogHelper.Log.Info("Click on Sort By DATE.");
    }

    [When(@"I click on the last Apply button")]
    public void WhenIClickOnTheLastApplyButton()
    {
        page.Click(JobListingsPage.LastApplyButton);
        LogHelper.Log.Info("Click on the last VIEW AND APPLY.");
    }
}
