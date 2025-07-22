using Business.UI.ApplicationInterface;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Business.UI.Steps;

[Binding]
public class SearchPageSteps
{
    private readonly SearchPage page;

    public SearchPageSteps(IWebDriver driver)
    {
        page = new SearchPage(driver);
    }

    [Then(@"All the elements of the search results contain '(.*)'")]
    public void ThenAllTheElementsOfTheSearchResultsContain(string searchTerm)
    {
        var results = page.GetSearchResults();
        Assert.That(results.Count > 0 && results.All(x => x.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)), Is.True);
    }
}
