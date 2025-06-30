using OpenQA.Selenium;

namespace WebApp.Pages
{
    public class JobDetail : PageBase
    {
        internal JobDetail(IWebDriver driver)
            : base(driver)
        {
        }

        public int GetNumberOfOccurances(string language)
        {
            try
            {
                return WaitHelp.WaitForAllElements(OccurancesOfLanguage(language)).Count;
            }
            catch (TimeoutException)
            {
                return 0;
            }
        }

        private static By OccurancesOfLanguage(string language) => By.XPath($"//article//*[contains(text(),'{language}')]");
    }
}