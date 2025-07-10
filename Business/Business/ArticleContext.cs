using Business.ApplicationInterface;

namespace Business.Business;

public class ArticleContext
{
    private readonly ArticlePage page;

    internal ArticleContext()
    {
        page = new ArticlePage();
    }

    public bool IsTitlePresent(string title)
    {
        return page.GetNumberOfOccurances(title) > 0;
    }
}
