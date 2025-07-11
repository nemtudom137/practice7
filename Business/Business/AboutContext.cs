using Business.ApplicationInterface;
using Core;

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
        LogHelper.Info("Scroll down to the EPAM at a Glance section.");
        PageBase.Click(AboutPage.DownloadButton);
        LogHelper.Info("Click on DOWNLOAD button.");
        page.WaitForDownload();
        LogHelper.Info("File downloaded.");
    }
}
