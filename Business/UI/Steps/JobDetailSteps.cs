using Business.UI.ApplicationInterface;
using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Business.UI.Steps;

[Binding]
public class JobDetailSteps
{
    private readonly JobDetailPage page;

    public JobDetailSteps()
    {
        page = new JobDetailPage();
    }

    [Then(@"The '(.*)' is on the resulting page")]
    public void ThenTheIsOnTheResultingPage(string language)
    {
        Assert.That(page.IsLanguagePresent(language), Is.True);
    }
}
