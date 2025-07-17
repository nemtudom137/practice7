using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Business.ApplicationInterface;

public class CareersPage : PageBase
{
    public static readonly By Keyword = By.CssSelector("input#new_form_job_search-keyword");
    public static readonly By FindButton = By.CssSelector("button[type='submit']");
    private static readonly By Locations = By.CssSelector(".recruiting-search__location span[role='combobox']");
    private static readonly By RemoteOption = By.CssSelector(".job-search__filter-list input[name='remote']");

    internal CareersPage(IWebDriver driver)
       : base(driver)
    {
    }

    public void SetLocation(string location)
    {
        Click(Locations);
        WaitHelper.ClickOnElement(GetLocationOption, location);
    }

    public void SetLocation(string location, string city)
    {
        Click(Locations);
        WaitHelper.ClickOnElement(GetLocationOptionGroup, location);
        WaitHelper.ClickOnElement(GetLocationOption, city);
    }

    public void ChooseRemote()
    {
        var remote = Driver.FindElement(RemoteOption);
        new Actions(Driver).MoveToElement(remote)
           .Click()
           .Perform();

        Log.Trace($"Element located by {remote} is clicked");
    }

    private static By GetLocationOption(string location) => By.XPath($"//li[contains(text(),'{location}')]");

    private static By GetLocationOptionGroup(string location) => By.XPath($"//strong[contains(text(),'{location}')]");
}
