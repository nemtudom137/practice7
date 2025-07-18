using Business.ApplicationInterface;
using Core;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Business.Steps;

[Binding]
public class AboutPageSteps
{
    private readonly AboutPage page;

    public AboutPageSteps(IWebDriver driver)
    {
        page = new AboutPage(driver);
    }

    [Given(@"I scroll down to the EPAM at a Glance section")]
    public void GivenIScrollDownToTheEPAMAtAGlanceSection()
    {
        page.ScrollToElement(AboutPage.EpamAtAGlance);
    }

    [When(@"I click on the Download button")]
    public void WhenIClickOnTheDownloadButton()
    {
        page.ScrollToElement(AboutPage.EpamAtAGlance);
        page.Click(AboutPage.DownloadButton);
    }

    [Then(@"A file named '(.*)' is downloaded")]
    public void ThenAFileNamedIsDownloaded(string fileName)
    {
        page.WaitForDownload();

        var downloadedFiles = Directory.GetFiles(ConfigurationManager.Test.DownloadDirectory);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(downloadedFiles.Length, Is.EqualTo(1));
            Assert.That(Path.GetFileName(downloadedFiles[0]), Is.EqualTo(fileName));
        }
    }
}
