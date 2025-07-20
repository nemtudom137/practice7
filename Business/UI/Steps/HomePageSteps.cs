using Business.UI.ApplicationInterface;
using Business.UI.ApplicationInterface.Sections;
using Core;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Business.UI.Steps;

[Binding]
public class HomePageSteps
{
    private readonly HomePage page;

    public HomePageSteps()
    {
        page = new HomePage();
    }

    [Given(@"I navigate to the EPAM website")]
    public void GivenINavigateToEPAMWebsite()
    {
        page.Open();
        LogHelper.Log.Info("Home page is open");
    }

    [Given(@"I click on Accept Cookies button")]
    public void GivenIClickOnAcceptCookiesButton()
    {
        page.ClickWithWait(HomePage.AcceptCookies);
        LogHelper.Log.Info("Cookies are accepted");
    }

    [Given(@"I'm on the Insights page")]
    public void GivenImOnTheInsightsPage()
    {
        page.Click(Header.Insights);
        LogHelper.Log.Info("Insights page is open");
    }

    [Given(@"I navigate to Carriers page")]
    public void GivenINavigateToCarriersPage()
    {
        page.Click(Header.Carriers);
        LogHelper.Log.Info("Careers page is open");
    }

    [Given(@"I'm on the About page")]
    public void GivenImOnTheAboutPage()
    {
        page.Click(Header.About);
        LogHelper.Log.Info("About page is open");
    }

    [When(@"I click on the Search icon element")]
    public void WhenIClickOnTheSearchIconElement()
    {
        page.Click(Header.SearchIcon);
        LogHelper.Log.Info("Click on Search icon.");
    }

    [When(@"I enter the text '(.*)' into the search input")]
    public void WhenIEnterTheTextIntoTheSearchInput(string searchTerm)
    {
        page.SetField(Header.SearchInput, searchTerm);
        LogHelper.Log.Info($"Search field is set to {searchTerm}");
    }

    [When(@"I click on the Find button")]
    public void WhenIClickOnTheFindButton()
    {
        page.Click(Header.FindButton);
        LogHelper.Log.Info("Click on FIND button.");
    }

    [When(@"I hover over Services link in the main navigation")]
    public void WhenIHoverOverServicesLinkInTheMainNavigation()
    {
        page.HoverOverElement(Header.Services);
        LogHelper.Log.Info("Hover over Services.");
    }

    [When(@"I click on the service category '(.*)'")]
    public void WhenIClickOnTheServiceCategory(string category)
    {
        page.ClickWithWait(Header.DropdownMenuItem(category));
        LogHelper.Log.Info($"Click on {category} service category.");
    }
}