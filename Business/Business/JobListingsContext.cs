using Business.ApplicationInterface;

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
        return this;
    }

    public JobDetailContext ClicOnTheLastApplyButton()
    {
        page.ClickWithWait(JobListingsPage.LastApplyButton);
        return new JobDetailContext();
    }
}
