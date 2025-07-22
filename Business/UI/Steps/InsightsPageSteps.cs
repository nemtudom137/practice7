using Business.UI.ApplicationInterface;
using Core;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Business.UI.Steps;

[Binding]
public class InsightsPageSteps
{
    private readonly InsightsPage page;
    private readonly ScenarioContext scenarioContext;

    public InsightsPageSteps(IWebDriver driver, ScenarioContext scenarioContext)
    {
        page = new InsightsPage(driver);
        this.scenarioContext = scenarioContext;
    }

    [When(@"I swipe (.*) the topmost carousel")]
    public void WhenISwipeTheTopmostCarousel(int n)
    {
        page.TopmostCarousel.Swipe(n);
        LogHelper.Log.Info($"Swipe {n} times on the carousel.");
    }

    [When(@"I note the name of the article")]
    public void WhenINoteTheNameOfTheArticle()
    {
        var carousel = page.TopmostCarousel;
        var title = page.GetElementText(carousel.ActiveSlideText);
        LogHelper.Log.Info($"Title on the active slide: {title}");
        scenarioContext.Set(title, TestInfoHelper.GetTestName());
    }

    [When(@"I click on the Read More button")]
    public void WhenIClickOnTheReadMoreButton()
    {
        page.Click(page.TopmostCarousel.ActiveSlideContetnLink);
        LogHelper.Log.Info("Click Read More button on the slide.");
    }
}
