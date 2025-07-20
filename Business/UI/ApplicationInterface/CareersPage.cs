using OpenQA.Selenium;

namespace Business.UI.ApplicationInterface;

public class CareersPage : PageBase
{
    public static readonly By Keyword = By.CssSelector("input#new_form_job_search-keyword");
    public static readonly By FindButton = By.CssSelector("button[type='submit']");
    public static readonly By LocationCombobox = By.CssSelector(".recruiting-search__location span[role='combobox']");
    public static readonly By RemoteOption = By.CssSelector(".job-search__filter-list input[name='remote']");

    internal CareersPage(IWebDriver driver)
       : base(driver)
    {
    }

    public static By GetSingleLocation(string location) => By.XPath($"//li[contains(text(),'{location}')]");

    public static By GetLocationOptionGroup(string location) => By.XPath($"//strong[contains(text(),'{location}')]");
}
