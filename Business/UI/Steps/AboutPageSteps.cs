using Business.UI.ApplicationInterface;
using Core;
using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Business.UI.Steps;

[Binding]
public class AboutPageSteps
{
    private readonly AboutPage page;

    public AboutPageSteps()
    {
        page = new AboutPage();
    }

    [Given(@"I scroll down to the EPAM at a Glance section")]
    public void GivenIScrollDownToTheEPAMAtAGlanceSection()
    {
        page.ScrollToElement(AboutPage.EpamAtAGlance);
        LogHelper.Log.Info("Scroll down to the EPAM at a Glance section.");
    }

    [When(@"I click on the Download button")]
    public void WhenIClickOnTheDownloadButton()
    {
        page.Click(AboutPage.DownloadButton);
        LogHelper.Log.Info("Click on DOWNLOAD button.");
    }

    [Then(@"A file named '(.*)' is downloaded")]
    public void ThenAFileNamedIsDownloaded(string fileName)
    {
        page.WaitForDownload();
        LogHelper.Log.Info("File downloaded.");

        var downloadedFiles = Directory.GetFiles(ConfigurationManager.UI.DownloadDirectory);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(downloadedFiles.Length, Is.EqualTo(1));
            Assert.That(Path.GetFileName(downloadedFiles[0]), Is.EqualTo(fileName));
        }
    }
}
