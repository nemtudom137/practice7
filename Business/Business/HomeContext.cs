using Business.ApplicationInterface;

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
        return new CareersContext();
    }

    public AboutContext GoToAbout()
    {
        PageBase.Click(Header.About);
        return new AboutContext();
    }

    public InsightsContext GoToInsights()
    {
        PageBase.Click(Header.Insights);
        return new InsightsContext();
    }

    public SearchContext SearchOnHeader(string searchTerms)
    {
        PageBase.Click(Header.SearchIcon);
        page.SetField(Header.SearchInput, searchTerms);
        PageBase.Click(Header.FindButton);
        return new SearchContext();
    }

    private HomeContext AcceptCookies()
    {
        page.ClickWithWait(HomePage.AcceptCookies);
        return this;
    }
}