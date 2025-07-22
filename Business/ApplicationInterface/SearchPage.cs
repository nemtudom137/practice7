using OpenQA.Selenium;

namespace Business.ApplicationInterface;

public class SearchPage : PageBase
{
    private static readonly By SearchResultItem = By.XPath("//article[@class='search-results__item']");
    private static readonly By ViewMoreButton = By.ClassName("search-results__view-more");

    internal SearchPage(IWebDriver driver)
        : base(driver)
    {
    }

    public List<string> GetSearchResults()
    {
        while (true)
        {
            ScrollDownDynamicElements(SearchResultItem);

            try
            {
                ClickOnDynamicElement(ViewMoreButton);
                Log.Trace($"Element located by {ViewMoreButton} is clicked");
            }
            catch (ElementNotInteractableException)
            {
                break;
            }
        }

        return Driver.FindElements(SearchResultItem).Select(x => x.Text).ToList();
    }
}
