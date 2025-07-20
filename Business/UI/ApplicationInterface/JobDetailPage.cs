using Core;
using OpenQA.Selenium;

namespace Business.UI.ApplicationInterface;

public class JobDetailPage : PageBase
{
    internal JobDetailPage(IWebDriver driver)
        : base(driver)
    {
    }

    public bool IsLanguagePresent(string language) => WaitHelper.IsElementPresent(OccurancesOfLanguage(language));

    private static By OccurancesOfLanguage(string language) => By.XPath($"//article//*[contains(text(),'{language}')]");
}
