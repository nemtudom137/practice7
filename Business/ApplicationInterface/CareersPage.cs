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

    public void SetLocation(string location)
    {
        Click(Locations);
        ClickWithWait(GetLocationOption(location));
    }

    public void SetLocation(string location, string city)
    {
        Click(Locations);
        ClickWithWait(GetLocationOptionGroup(location));
        ClickWithWait(GetLocationOption(city));
    }

    public void ChooseRemote()
    {
        var remote = DriverContainer.Driver.FindElement(RemoteOption);
        new Actions(DriverContainer.Driver).MoveToElement(remote)
           .Click()
           .Perform();
    }

    private static By GetLocationOption(string location) => By.XPath($"//li[contains(text(),'{location}')]");

    private static By GetLocationOptionGroup(string location) => By.XPath($"//strong[contains(text(),'{location}')]");
}
