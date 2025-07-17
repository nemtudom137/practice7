using Business.ApplicationInterface;
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
        Log.Info($"Keyword field is set to {searchTerms}");
        return this;
    }

    public CareersContext SetLocation(string location, string city)
    {
        if (string.IsNullOrEmpty(city))
        {
            page.SetLocation(location);
            Log.Info($"Location is set to {location}");
        }
        else
        {
            page.SetLocation(location, city);
            Log.Info($"Location is set to {location} - {city}");
        }

        return this;
    }

    public CareersContext ChooseRemote()
    {
        page.ChooseRemote();
        Log.Info($"Remote is chosen");
        return this;
    }

    public JobListingsContext ClickOnFind()
    {
        page.Click(CareersPage.FindButton);
        Log.Info("Click on FIND button.");
        return new JobListingsContext(Driver);
    }
}
