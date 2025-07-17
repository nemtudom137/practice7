using Core;
using OpenQA.Selenium;

namespace Business.ApplicationInterface;

public class ArticlePage : PageBase
{
    private static readonly By Titles = By.XPath($"//p[@class='scaling-of-text-wrapper']");

    internal ArticlePage(IWebDriver driver)
        : base(driver)
    {
    }

    public int GetNumberOfOccurances(string title)
    {
        int n;
        try
        {
            n = WaitHelper.WaitForAnyElement(Titles, x => x.Text.Contains(title)).Count;
        }
        catch (WebDriverTimeoutException)
        {
            n = 0;
        }

        Log.Trace($"Number of occurancs of {title}: {n}");
        return n;
    }
}
