namespace Business.ApplicationInterface;

public class InsightsPage : PageBase
{
    private static readonly string FirstCarousel = "//div[@class='slider section'][1]";

    internal InsightsPage()
    {
        TopmostCarousel = new Carousel(FirstCarousel);
    }

    public Carousel TopmostCarousel { get; init; }
}