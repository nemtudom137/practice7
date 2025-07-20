using Business.UI.ApplicationInterface;
using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Business.UI.ApplicationInterface.Sections;

public class Carousel : PageBase
{
    private readonly string root;

    internal Carousel(string root)
        : base()
    {
        this.root = root;
    }

    public By RightArrow => By.XPath($"{root}//button[contains(@class,'slider__right-arrow')]");

    public By ActiveSlideText => By.XPath($"{root}//div[@aria-hidden='false']//div[@class ='text']");

    public By ActiveSlideContetnLink => By.XPath($"{root}//div[@aria-hidden='false']//a");

    public void Swipe(int time)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(time);
        var arrow = WaitHelper.WaitForElementToBeDispayed(RightArrow);
        var action = new Actions(Driver).MoveToElement(arrow);
        for (int i = 0; i < time; i++)
        {
            action.Click()
                .Pause(TimeSpan.FromMilliseconds(500))
                .Perform();
        }
    }
}