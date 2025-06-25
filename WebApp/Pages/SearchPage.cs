using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace WebApp.Pages;

public class SearchPage : BasePage
{
    private readonly By searchResultItem = By.XPath("//article[@class='search-results__item']");
    private readonly By viewMoreButton = By.ClassName("search-results__view-more");
    internal SearchPage(IWebDriver driver) : base(driver)
    {
    }    

    public List<string> GetSearchResults()
    {
        var action = new Actions(Driver);

        while (true)
        {
            ScrollDownSearchResults(searchResultItem);
            try
            {
                var button = WaitHelp.Wait.Until(d => d.FindElement(viewMoreButton));
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

        return Driver.FindElements(searchResultItem).Select(x => x.Text).ToList();
    }

    private void ScrollDownSearchResults(By resultElement)
    {
        var action = new Actions(Driver);

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

            WaitHelp.Wait.Until(d =>
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