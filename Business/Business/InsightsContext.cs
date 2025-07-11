using Business.ApplicationInterface;
using Core;

namespace Business.Business;

public class InsightsContext
{
    private readonly InsightsPage page;

    internal InsightsContext()
    {
        page = new InsightsPage();
    }

    public ArticleContext GoToSlidelArticle()
    {
        page.ClickWithWait(page.TopmostCarousel.ActiveSlideContetnLink);
        LogHelper.Info("Click Read More button on the slide.");
        return new ArticleContext();
    }

    public string GetTopmostCarouselTitle()
    {
        var title = page.TopmostCarousel.GetActivArticleTitle();
        LogHelper.Info($"Title on the active slide: {title}");
        return title;
    }

    public InsightsContext SwipeTheTopmostCarousel(int times)
    {
        page.TopmostCarousel.Swipe(times);
        LogHelper.Info($"Swipe {times} times on the carousel.");
        return this;
    }
}
