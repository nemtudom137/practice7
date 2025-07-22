using OpenQA.Selenium;

namespace Business.ApplicationInterface;

public class ArticlePage : PageBase
{
    public static readonly By Titles = By.XPath($"//p[@class='scaling-of-text-wrapper']");

    internal ArticlePage(IWebDriver driver)
        : base(driver)
    {
    }
}
