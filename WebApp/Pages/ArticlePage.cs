using OpenQA.Selenium;

namespace WebApp.Pages;

public class ArticlePage : BasePage
{
    private readonly By titleOccurances = By.XPath($"//p[@class='scaling-of-text-wrapper']");
    internal ArticlePage(IWebDriver driver) : base(driver, TimeSpan.FromSeconds(5))
    {
    }

    public bool GetNumberOfOccurances(string title)
    {
        return WaitHelp.AnyElementWith(titleOccurances, x => x.Text.Contains(title));
    }
}
