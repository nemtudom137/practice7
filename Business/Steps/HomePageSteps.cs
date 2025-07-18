using Business.ApplicationInterface;
using Business.ApplicationInterface.Sections;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Business.Steps;

[Binding]
public class HomePageSteps
{
    private readonly HomePage page;

    public HomePageSteps(IWebDriver driver)
    {
        page = new HomePage(driver);
    }

    [Given(@"I navigate to the EPAM website")]
    public void GivenINavigateToEPAMWebsite() => page.Open();

    [Given(@"I click on Accept Cookies button")]
    public void GivenIClickOnAcceptCookiesButton()
    {
        page.ClickWithWait(HomePage.AcceptCookies);
    }

    [Given(@"I'm on the Insights page")]
    public void GivenImOnTheInsightsPage()
    {
        page.Click(Header.Insights);
    }

    [Given(@"I navigate to Carriers page")]
    public void GivenINavigateToCarriersPage()
    {
        page.Click(Header.Carriers);
    }

    [Given(@"I'm on the About page")]
    public void GivenImOnTheAboutPage()
    {
        page.Click(Header.About);
    }

    [When(@"I click on the Search icon element")]
    public void WhenIClickOnTheSearchIconElement()
    {
        page.Click(Header.SearchIcon);
    }

    [When(@"I enter the text '(.*)' into the search input")]
    public void WhenIEnterTheTextIntoTheSearchInput(string searchTerm)
    {
        page.SetField(Header.SearchInput, searchTerm);
    }

    [When(@"I click on the Find button")]
    public void WhenIClickOnTheFindButton()
    {
        page.Click(Header.FindButton);
    }

    [When(@"I hover over Services link in the main navigation")]
    public void WhenIHoverOverServicesLinkInTheMainNavigation()
    {
        page.HoverOverElement(Header.Services);
    }

    [When(@"I click on the service category '(.*)'")]
    public void WhenIClickOnTheServiceCategory(string category)
    {
        page.ClickWithWait(Header.DropdownMenuItem(category));
    }
}