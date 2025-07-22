using Business.ApplicationInterface;
using Core;
using OpenQA.Selenium;

namespace Business.Business;

public class SearchContext : ContextBase
{
    private readonly SearchPage page;

    public SearchContext(IWebDriver? driver)
        : base(driver)
    {
        page = new SearchPage(Driver);
    }

    public List<string> GetSearchResults()
    {
        var results = page.GetSearchResults();
        LogHelper.Log.Info($"{results.Count} search results was found.");

        return results;
    }
}
