using OpenQA.Selenium;

namespace Business.ApplicationInterface;

public class JobDetailPage : PageBase
{
    public int GetNumberOfOccurances(string language)
    {
        try
        {
            return WaitHelper.WaitForAnyElement(OccurancesOfLanguage(language)).Count;
        }
        catch (WebDriverTimeoutException)
        {
            return 0;
        }
    }

    private static By OccurancesOfLanguage(string language) => By.XPath($"//article//*[contains(text(),'{language}')]");
}
