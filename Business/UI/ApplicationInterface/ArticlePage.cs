using Core;
using OpenQA.Selenium;

namespace Business.UI.ApplicationInterface;

public class ArticlePage : PageBase
{
    private static readonly By Titles = By.XPath($"//p[@class='scaling-of-text-wrapper']");

    internal ArticlePage(IWebDriver driver)
        : base(driver)
    {
    }

    public bool IsTitlePresent(string title)
    {
        return WaitHelper.AnyElementDisplayed(Titles, x => x.Text.Contains(title));
    }

    public bool IsSectionPresent(string title) => WaitHelper.IsElementPresent(Section(title));

    private static By Section(string title) => By.XPath($"//div[@class='section']//span[contains(text(),'{title}')]");
}
