using OpenQA.Selenium;
using WebApp.Pages.Sections;

namespace WebApp.Pages;

public class InsightsPage : BasePage
{
    private readonly string topmostCarousel = "//div[@class='slider section'][1]";

    internal InsightsPage(IWebDriver driver) : base(driver)
    {
        TopmostCarousel = new Carousel(Driver, topmostCarousel);
    }

    public Carousel TopmostCarousel { get; init; }
}