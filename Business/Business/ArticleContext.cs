using Business.ApplicationInterface;
using OpenQA.Selenium;

namespace Business.Business;

public class ArticleContext : ContextBase
{
    private readonly ArticlePage page;

    internal ArticleContext(IWebDriver? driver)
        : base(driver)
    {
        page = new ArticlePage(Driver);
    }

    public bool IsTitlePresent(string title)
    {
        return page.GetNumberOfOccurances(title) > 0;
    }
}
