using OpenQA.Selenium;

namespace Business.UI.ApplicationInterface;

public class JobListingsPage : PageBase
{
    public static readonly By SortByDate = By.CssSelector("#sort-time ~ label");

    public static readonly By LastApplyButton = By.XPath("//ul[@class='search-result__list']/li[last()]//a[contains(text(),'apply')]");
}