using Business.ApplicationInterface;
using Core;
using OpenQA.Selenium;

namespace Business.Business;

public class InsightsContext : ContextBase
{
    private readonly InsightsPage page;

    internal InsightsContext(IWebDriver? driver)
        : base(driver)
    {
        page = new InsightsPage(Driver);
    }

    public ArticleContext GoToSlidelArticle()
    {
        page.ClickWithWait(page.TopmostCarousel.ActiveSlideContetnLink);
        Log.Info("Click Read More button on the slide.");
        return new ArticleContext(Driver);
    }

    public string GetTopmostCarouselTitle()
    {
        var title = page.TopmostCarousel.GetActivArticleTitle();
        Log.Info($"Title on the active slide: {title}");
        return title;
    }

    public InsightsContext SwipeTheTopmostCarousel(int times)
    {
        page.TopmostCarousel.Swipe(times);
        Log.Info($"Swipe {times} times on the carousel.");
        return this;
    }
}
