using OpenQA.Selenium;

namespace WebApp.Pages;

public class SearchPage : BasePage
{
    private readonly By searchResultItem = By.XPath("//article[@class='search-results__item']");

    internal SearchPage(IWebDriver driver) : base(driver)
    {
    }

    public List<string> GetSearchResults()
    {
        return Wait.WaitForAllElements(searchResultItem).Select(x => x.Text).ToList();
    }
}