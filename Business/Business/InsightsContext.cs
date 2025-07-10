using Business.ApplicationInterface;

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
        return new ArticleContext();
    }

    public string GetTopmostCarouselTitle()
    {
        return page.TopmostCarousel.GetActivArticleTitle();
    }

    public InsightsContext SwipeTheTopmostCarousel(int times)
    {
        page.TopmostCarousel.Swipe(times);
        return this;
    }
}
