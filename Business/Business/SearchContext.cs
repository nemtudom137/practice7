using Business.ApplicationInterface;
using Core;

namespace Business.Business;

public class SearchContext
{
    private readonly SearchPage page;

    public SearchContext()
    {
        page = new SearchPage();
    }

    public List<string> GetSearchResults()
    {
        var results = page.GetSearchResults();
        LogHelper.Info($"{results.Count} search results was found.");
        return results;
    }
}
