using Business.ApplicationInterface;

namespace Business.Business;

public class AboutContext
{
    private readonly AboutPage page;

    internal AboutContext()
    {
        page = new AboutPage();
    }

    public void DownloadCompanyOverview()
    {
        PageBase.ScrollToElement(AboutPage.EpamAtAGlance);
        PageBase.Click(AboutPage.DownloadButton);
        page.WaitForDownload();
    }
}
