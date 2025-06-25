using OpenQA.Selenium;
using WebApp.Pages.Sections;

namespace WebApp.Pages;

public class InsightsPage : BasePage
{
    private readonly string topmostCarouselPath = "//div[@class='slider section'][1]";

    internal InsightsPage(IWebDriver driver) : base(driver)
    {
        TopmostCarousel = new Carousel(Driver, topmostCarouselPath);
    }

    public Carousel TopmostCarousel { get; init; }
}