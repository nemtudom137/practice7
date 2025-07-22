using Business.ApplicationInterface;
using Core;
using OpenQA.Selenium;

namespace Business.Business;

public class JobListingsContext : ContextBase
{
    private readonly JobListingsPage page;

    public JobListingsContext(IWebDriver? driver)
        : base(driver)
    {
        page = new JobListingsPage(Driver);
    }

    public JobListingsContext SortResultByDate()
    {
        page.Click(JobListingsPage.SortByDate);
        LogHelper.Log.Info("Click on Sort By DATE.");

        return this;
    }

    public JobDetailContext ClicOnTheLastApplyButton()
    {
        page.Click(JobListingsPage.LastApplyButton);
        LogHelper.Log.Info("Click on the last VIEW AND APPLY.");

        return new JobDetailContext(Driver);
    }
}
