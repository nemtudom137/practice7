using Business.ApplicationInterface;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Business.Steps;

[Binding]
public class CareersPageSteps
{
    private readonly CareersPage page;

    public CareersPageSteps(IWebDriver driver)
    {
        page = new CareersPage(driver);
    }

    [When(@"I enter the text '(.*)' into the search field")]
    public void WhenIEnterTheTextIntoTheSearchField(string language)
    {
        page.SetField(CareersPage.Keyword, language);
    }

    [When(@"I set the location to '(.*)' '(.*)'")]
    public void WhenISetTheLocationTo(string location, string city)
    {
        if (string.IsNullOrEmpty(city))
        {
            page.SetLocation(location);
        }
        else
        {
            page.SetLocation(location, city);
        }
    }

    [When(@"I chose remote")]
    public void WhenIChoseRemote()
    {
        page.ChooseRemote();
    }

    [When(@"I click on Find button")]
    public void WhenIClickOnFindButton()
    {
        page.Click(CareersPage.FindButton);
    }
}
