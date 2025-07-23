using Business.ApplicationInterface;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Business.Steps;

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
        var slideTitle = scenarioContext.Get<string>("slideTitle");
        Assert.That(page.IsTextPresentInElement(ArticlePage.Titles, slideTitle), Is.True);
    }

    [Then(@"I should see the article about '(.*)'")]
    public void ThenIShouldSeeTheArticleAbout(string category)
    {
        Assert.That(page.IsTextPresentInElement(ArticlePage.Titles, category), Is.True);
    }

    [Then(@"The section '(.*)' is displayed on the page")]
    public void ThenTheSectionIsDisplayedOnThePage(string section)
    {
        Assert.That(page.IsElementPresent(ArticlePage.Section(section)), Is.True);
    }
}
