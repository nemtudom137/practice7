using Business.ApplicationInterface;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Business.Steps;

[Binding]
public class JobDetailSteps
{
    private readonly JobDetailPage page;

    public JobDetailSteps(IWebDriver driver)
    {
        page = new JobDetailPage(driver);
    }

    [Then(@"The '(.*)' is on the resulting page")]
    public void ThenTheIsOnTheResultingPage(string language)
    {
        Assert.That(page.IsLanguagePresent(language), Is.True);
    }
}
