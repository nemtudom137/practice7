using Business.Business;
using Core;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework.Internal;

namespace Tests;

public class Tests : TestsBase
{
    [Test]
    [TestCaseSource(typeof(TestData), nameof(TestData.CarrierSearch))]
    public void CarrierSearchResult_ContainsLanguage(string language, string location, string city)
    {
        var languageIsFound = HomeContext.Open(Driver)
             .GoToCareers()
             .SetSearchTerms(language)
             .SetLocation(location, city)
             .ChooseRemote()
             .ClickOnFind()
             .SortResultByDate()
             .ClicOnTheLastApplyButton()
             .IsLanguagePresent(language);

        Assert.That(languageIsFound, Is.True);
    }

    [Test]
    [TestCaseSource(typeof(TestData), nameof(TestData.GlobalSearch))]
    public void GlobalSearchFirstResults_ContainKeyword(string searchString)
    {
        var results = HomeContext.Open(Driver)
            .SearchOnHeader(searchString)
            .GetSearchResults();

        Assert.That(results.Count > 0 && results.All(x => x.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)), Is.True);
    }

    [Test]
    [TestCase(TestData.CompanyOverviewFileName)]
    public void DownloadOnAboutPage_GivesFileWithCorrectName(string expectedFileName)
    {
        HomeContext.Open(Driver).GoToAbout().DownloadCompanyOverview();

        var downloadedFiles = Directory.GetFiles(ConfigurationManager.UI.DownloadDirectory);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(downloadedFiles.Length, Is.EqualTo(1));
            Assert.That(Path.GetFileName(downloadedFiles[0]), Is.EqualTo(expectedFileName));
        }
    }

    [Test]
    [TestCaseSource(typeof(TestData), nameof(TestData.SwipeTimes))]
    public void CarouselOnInsightsPage_RedirectToArticleWithCorrectName(int times)
    {
        var insightsPage = HomeContext.Open(Driver).GoToInsights();
        var articleTitle = insightsPage
            .SwipeTheTopmostCarousel(times)
            .GetTopmostCarouselTitle();
        var isTitlePresentInTheArticle = insightsPage.GoToSlidelArticle().IsTitlePresent(articleTitle);

        Assert.That(isTitlePresentInTheArticle, Is.True);
    }
}
