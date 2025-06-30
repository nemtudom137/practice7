using OpenQA.Selenium;

namespace WebApp.Pages;

public class ArticlePage : PageBase
{
    private readonly By titles = By.XPath($"//p[@class='scaling-of-text-wrapper']");

    internal ArticlePage(IWebDriver driver)
        : base(driver)
    {
    }

    public bool GetNumberOfOccurances(string title)
    {
        return WaitHelp.AnyElementDisplayed(titles, x => x.Text.Contains(title));
    }
}
