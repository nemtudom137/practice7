using Core;
using OpenQA.Selenium;

namespace Business.ApplicationInterface;

public class ArticlePage : PageBase
{
    private static readonly By Titles = By.XPath($"//p[@class='scaling-of-text-wrapper']");

    public int GetNumberOfOccurances(string title)
    {
        try
        {
            return WaitHelper.WaitForAnyElement(Titles, x => x.Text.Contains(title)).Count;
        }
        catch (WebDriverTimeoutException)
        {
            return 0;
        }
    }
}
