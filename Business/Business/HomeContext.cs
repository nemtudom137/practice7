using Business.ApplicationInterface;
using Core;

namespace Business.Business;

public class HomeContext
{
    private readonly HomePage page;

    private HomeContext()
    {
        page = new HomePage();
    }

    public static HomeContext Open()
    {
        var context = new HomeContext().AcceptCookies();
        return context;
    }

    public CareersContext GoToCareers()
    {
        PageBase.Click(Header.Carriers);
        LogHelper.Info("Careers page is open");
        return new CareersContext();
    }

    public AboutContext GoToAbout()
    {
        PageBase.Click(Header.About);
        LogHelper.Info("About page is open");
        return new AboutContext();
    }

    public InsightsContext GoToInsights()
    {
        PageBase.Click(Header.Insights);
        LogHelper.Info("Insights page is open");
        return new InsightsContext();
    }

    public SearchContext SearchOnHeader(string searchTerms)
    {
        PageBase.Click(Header.SearchIcon);
        page.SetField(Header.SearchInput, searchTerms);
        LogHelper.Info($"Search field is set to {searchTerms}");
        PageBase.Click(Header.FindButton);
        LogHelper.Info("Click on FIND button.");
        return new SearchContext();
    }

    private HomeContext AcceptCookies()
    {
        page.ClickWithWait(HomePage.AcceptCookies);
        LogHelper.Info("Click on Accept Cookies button.");
        return this;
    }
}