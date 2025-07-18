using Business.ApplicationInterface;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Business.Steps;

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
    }

    [When(@"I note the name of the article")]
    public void WhenINoteTheNameOfTheArticle()
    {
        scenarioContext.Set<string>(page.TopmostCarousel.GetActivArticleTitle(), "slideTitle");
    }

    [When(@"I click on the Read More button")]
    public void WhenIClickOnTheReadMoreButton()
    {
        page.ClickWithWait(page.TopmostCarousel.ActiveSlideContetnLink);
    }
}
