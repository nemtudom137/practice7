using Business.ApplicationInterface;
using OpenQA.Selenium;

namespace Business.Business;

public class AboutContext : ContextBase
{
    private readonly AboutPage page;

    internal AboutContext(IWebDriver? driver)
        : base(driver)
    {
        page = new AboutPage(Driver);
    }

    public void DownloadCompanyOverview()
    {
        page.ScrollToElement(AboutPage.EpamAtAGlance);
        Log.Info("Scroll down to the EPAM at a Glance section.");
        page.Click(AboutPage.DownloadButton);
        Log.Info("Click on DOWNLOAD button.");
        page.WaitForDownload();
        Log.Info("File downloaded.");
    }
}
