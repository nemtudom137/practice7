using Business.ApplicationInterface;
using Core;

namespace Business.Business;

public class CareersContext
{
    private readonly CareersPage page;

    internal CareersContext()
    {
        page = new CareersPage();
    }

    public CareersContext SetSearchTerms(string searchTerms)
    {
        page.SetField(CareersPage.Keyword, searchTerms);
        LogHelper.Info($"Keyword field is set to {searchTerms}");
        return this;
    }

    public CareersContext SetLocation(string location, string city)
    {
        if (string.IsNullOrEmpty(city))
        {
            page.SetLocation(location);
            LogHelper.Info($"Location is set to {location}");
        }
        else
        {
            page.SetLocation(location, city);
            LogHelper.Info($"Location is set to {location} - {city}");
        }

        return this;
    }

    public CareersContext ChooseRemote()
    {
        page.ChooseRemote();
        LogHelper.Info($"Remote is chosen");
        return this;
    }

    public JobListingsContext ClickOnFind()
    {
        PageBase.Click(CareersPage.FindButton);
        LogHelper.Info("Click on FIND button.");
        return new JobListingsContext();
    }
}
