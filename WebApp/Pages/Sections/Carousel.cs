using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace WebApp.Pages.Sections;

public class Carousel : PageObjectBase
{
    private By RightArrow => By.XPath($"{root}//button[contains(@class,'slider__right-arrow')]");
    private By ActiveSlideText => By.XPath($"{root}//div[@aria-hidden='false']//div[@class ='text']");
    private By ActiveSlideContetnLink => By.XPath($"{root}//div[@aria-hidden='false']//a");
    private readonly string root;

    internal Carousel(IWebDriver driver, string root) : base(driver)
    {
        this.root = root;
    }

    public Carousel Swipe(int time)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(time);
        var arrow = Wait.WaitForDisplayElement(RightArrow);
        for (int i = 0; i < time; i++)
        {
            new Actions(Driver).MoveToElement(arrow)
                .Click()
                .Pause(TimeSpan.FromMilliseconds(500))
                .Perform();
        }

        return this;
    }

    public string GetActivArticleTitle() => new WaitHelper(Driver, TimeSpan.FromSeconds(2)).WaitForDisplayElement(ActiveSlideText).Text;

    public ArticlePage ClickOnReadMore()
    {
        Wait.ClickOnElement(ActiveSlideContetnLink);
        return new ArticlePage(Driver);
    }
}

