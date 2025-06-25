using OpenQA.Selenium;

namespace WebApp.Pages
{
    public class JobDetail : BasePage
    {
        private static By OccurancesOfLanguage(string language) => By.XPath($"//article//*[contains(text(),'{language}')]");
        internal JobDetail(IWebDriver driver) : base(driver)
        {
        }

        public int GetNumberOfOccurances(string language)
        {
            var results = Wait.WaitForAllElements(OccurancesOfLanguage(language));
            return results.Count;
        }
    }
}