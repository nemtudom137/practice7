using Business.ApplicationInterface;

namespace Business.Business;

public class SearchContext
{
    private readonly SearchPage page;

    public SearchContext()
    {
        page = new SearchPage();
    }

    public List<string> GetSearchResults() => page.GetSearchResults();
}
