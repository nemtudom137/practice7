using Business.ApplicationInterface;
using Core;
using NLog;
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
        LogHelper.Log.Info($"Keyword field is set to {language}");
    }

    [When(@"I set the location to '(.*)' '(.*)'")]
    public void WhenISetTheLocationTo(string location, string city)
    {
        page.Click(CareersPage.LocationCombobox);

        if (string.IsNullOrEmpty(city))
        {
            page.Click(CareersPage.GetSingleLocation(location));
            LogHelper.Log.Info($"Location is set to {location}");
        }
        else
        {
            page.Click(CareersPage.GetLocationOptionGroup(location));
            page.Click(CareersPage.GetSingleLocation(city));
            LogHelper.Log.Info($"Location is set to {location} - {city}");
        }
    }

    [When(@"I chose remote")]
    public void WhenIChoseRemote()
    {
        page.ChooseOption(CareersPage.RemoteOption);
        LogHelper.Log.Info($"Remote is chosen");
    }

    [When(@"I click on Find button")]
    public void WhenIClickOnFindButton()
    {
        page.Click(CareersPage.FindButton);
        LogHelper.Log.Info("Click on FIND button.");
    }
}
