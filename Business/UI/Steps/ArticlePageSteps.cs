using Business.UI.ApplicationInterface;
using Core;
using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Business.UI.Steps;

[Binding]
public class ArticlePageSteps
{
    private readonly ArticlePage page;
    private readonly ScenarioContext scenarioContext;

    public ArticlePageSteps(IWebDriver driver, ScenarioContext scenarioContext)
    {
        page = new ArticlePage(driver);
        this.scenarioContext = scenarioContext;
    }

    [Then(@"The article title should be the one noted earlier")]
    public void ThenTheArticleTitleShouldBeTheOneNotedEarlier()
    {
        var slideTitle = scenarioContext.Get<string>(TestInfoHelper.GetTestName());
        Assert.That(page.IsTitlePresent(slideTitle), Is.True);
    }

    [Then(@"I should see the article about '(.*)'")]
    public void ThenIShouldSeeTheArticleAbout(string category)
    {
        Assert.That(page.IsTitlePresent(category), Is.True);
    }

    [Then(@"The section '(.*)' is displayed on the page")]
    public void ThenTheSectionIsDisplayedOnThePage(string section)
    {
        Assert.That(page.IsSectionPresent(section), Is.True);
    }
}
