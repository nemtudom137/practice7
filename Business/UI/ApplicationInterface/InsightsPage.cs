using Business.UI.ApplicationInterface.Sections;
using OpenQA.Selenium;

namespace Business.UI.ApplicationInterface;

public class InsightsPage : PageBase
{
    private static readonly string FirstCarousel = "//div[@class='slider section'][1]";

    internal InsightsPage()
         : base()
    {
        TopmostCarousel = new Carousel(FirstCarousel);
    }

    public Carousel TopmostCarousel { get; init; }
}