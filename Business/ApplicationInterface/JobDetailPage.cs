using OpenQA.Selenium;

namespace Business.ApplicationInterface;

public class JobDetailPage : PageBase
{
    internal JobDetailPage(IWebDriver driver)
        : base(driver)
    {
    }

    public int GetNumberOfOccurances(string language)
    {
        int n;
        try
        {
            n = WaitHelper.WaitForAnyElement(OccurancesOfLanguage(language)).Count;
        }
        catch (WebDriverTimeoutException)
        {
            n = 0;
        }

        Log.Trace($"Number of occurancs of {language}: {n}");
        return n;
    }

    private static By OccurancesOfLanguage(string language) => By.XPath($"//article//*[contains(text(),'{language}')]");
}
