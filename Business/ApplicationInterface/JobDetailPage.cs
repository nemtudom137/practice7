using Core;
using OpenQA.Selenium;

namespace Business.ApplicationInterface;

public class JobDetailPage : PageBase
{
    internal JobDetailPage(IWebDriver driver)
        : base(driver)
    {
    }

    public static By OccurancesOfLanguage(string language) => By.XPath($"//article//*[contains(text(),'{language}')]");
}
