using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Business.ApplicationInterface;

public class SearchPage : PageBase
{
    private static readonly By SearchResultItem = By.XPath("//article[@class='search-results__item']");
    private static readonly By ViewMoreButton = By.ClassName("search-results__view-more");

    public List<string> GetSearchResults()
    {
        var action = new Actions(DriverContainer.Driver);

        while (true)
        {
            ScrollDownSearchResults(SearchResultItem);
            try
            {
                var button = WaitHelper.Wait.Until(d => d.FindElement(ViewMoreButton));
                action.ScrollToElement(button)
                .Pause(TimeSpan.FromSeconds(1))
                .MoveToElement(button)
                .Click()
                .Perform();
            }
            catch (ElementNotInteractableException)
            {
                break;
            }
        }

        return DriverContainer.Driver.FindElements(SearchResultItem).Select(x => x.Text).ToList();
    }

    private void ScrollDownSearchResults(By resultElement)
    {
        var action = new Actions(DriverContainer.Driver);

        int totalCount = 0;
        int currentCount = 0;

        do
        {
            totalCount = currentCount;

            action.Pause(TimeSpan.FromSeconds(1))
                .KeyDown(Keys.Control)
                .SendKeys(Keys.End)
                .KeyUp(Keys.Control)
                .Perform();

            WaitHelper.Wait.Until(d =>
            {
                var n = d.FindElements(resultElement).Count;
                if (n > currentCount)
                {
                    currentCount = n;
                    return false;
                }
                else
                {
                    return true;
                }
            });
        }
        while (totalCount > currentCount);
    }
}
