using OpenQA.Selenium;

namespace Business.ApplicationInterface;

public class ArticlePage : PageBase
{
    public static readonly By Titles = By.XPath($"//p[@class='scaling-of-text-wrapper']");

    internal ArticlePage(IWebDriver driver)
        : base(driver)
    {
    }

    public bool IsSectionPresent(string title) => WaitHelper.IsElementPresent(Section(title));

    private static By Section(string title) => By.XPath($"//div[@class='section']//span[contains(text(),'{title}')]");
}
