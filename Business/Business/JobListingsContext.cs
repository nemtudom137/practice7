using Business.ApplicationInterface;
using Core;

namespace Business.Business;

public class JobListingsContext
{
    private readonly JobListingsPage page;

    public JobListingsContext()
    {
        page = new JobListingsPage();
    }

    public JobListingsContext SortResultByDate()
    {
        page.ClickWithWait(JobListingsPage.SortByDate);
        LogHelper.Info("Click on Sort By DATE.");
        return this;
    }

    public JobDetailContext ClicOnTheLastApplyButton()
    {
        page.ClickWithWait(JobListingsPage.LastApplyButton);
        LogHelper.Info("Click on the last VIEW AND APPLY.");
        return new JobDetailContext();
    }
}
