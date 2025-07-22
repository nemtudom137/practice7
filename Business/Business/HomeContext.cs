using Business.ApplicationInterface;
using Core;
using OpenQA.Selenium;

namespace Business.Business;

public class HomeContext : ContextBase
{
    private readonly HomePage page;

    private HomeContext(IWebDriver? driver)
        : base(driver)
    {
        page = new HomePage(Driver);
        page.Open();
    }

    public static HomeContext Open(IWebDriver? driver)
    {
        var context = new HomeContext(driver).AcceptCookies();
        return context;
    }

    public CareersContext GoToCareers()
    {
        page.Click(Header.Carriers);
        LogHelper.Log.Info("Careers page is open");

        return new CareersContext(Driver);
    }

    public AboutContext GoToAbout()
    {
        page.Click(Header.About);
        LogHelper.Log.Info("About page is open");

        return new AboutContext(Driver);
    }

    public InsightsContext GoToInsights()
    {
        page.Click(Header.Insights);
        LogHelper.Log.Info("Insights page is open");

        return new InsightsContext(Driver);
    }

    public SearchContext SearchOnHeader(string searchTerms)
    {
        page.Click(Header.SearchIcon);
        page.SetField(Header.SearchInput, searchTerms);
        LogHelper.Log.Info($"Search field is set to {searchTerms}");

        page.Click(Header.FindButton);
        LogHelper.Log.Info("Click on FIND button.");

        return new SearchContext(Driver);
    }

    private HomeContext AcceptCookies()
    {
        page.Click(HomePage.AcceptCookies);
        LogHelper.Log.Info("Click on Accept Cookies button.");

        return this;
    }
}