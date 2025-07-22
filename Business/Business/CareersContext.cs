using Business.ApplicationInterface;
using Core;
using OpenQA.Selenium;

namespace Business.Business;

public class CareersContext : ContextBase
{
    private readonly CareersPage page;

    internal CareersContext(IWebDriver? driver)
        : base(driver)
    {
        page = new CareersPage(Driver);
    }

    public CareersContext SetSearchTerms(string searchTerms)
    {
        page.SetField(CareersPage.Keyword, searchTerms);
        LogHelper.Log.Info($"Keyword field is set to {searchTerms}");

        return this;
    }

    public CareersContext SetLocation(string location, string city)
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

        return this;
    }

    public CareersContext ChooseRemote()
    {
        page.ChooseOption(CareersPage.RemoteOption);
        LogHelper.Log.Info($"Remote is chosen");

        return this;
    }

    public JobListingsContext ClickOnFind()
    {
        page.Click(CareersPage.FindButton);
        LogHelper.Log.Info("Click on FIND button.");

        return new JobListingsContext(Driver);
    }
}
