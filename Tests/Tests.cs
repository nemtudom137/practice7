using Business.Business;
using Business.Data;
using Core;
using NUnit.Framework.Internal;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        if (Directory.Exists(ConfigurationReader.Test.TestDirectory))
        {
            Directory.Delete(ConfigurationReader.Test.TestDirectory, true);
        }

        Directory.CreateDirectory(ConfigurationReader.Test.TestDirectory);
    }

    [TearDown]
    public void TearDown()
    {
        DriverContainer.QuitDriver();
    }

    [Test]
    [TestCaseSource(typeof(TestData), nameof(TestData.CarrierSearch))]
    public void CarrierSearchResult_ContainsLanguage(string language, string location, string city)
    {
        LogHelper.Debug("aaaa");
        var languageIsFound = HomeContext.Open()
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
        var results = HomeContext.Open()
            .SearchOnHeader(searchString)
            .GetSearchResults();

        Assert.That(results.Count > 0 && results.All(x => x.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)), Is.True);
    }

    [Test]
    [TestCaseSource(typeof(TestData), nameof(TestData.CompanyOverviewFileName))]
    public void DownloadOnAboutPage_GivesFileWithCorrectName(string expectedFileName)
    {
        HomeContext.Open().GoToAbout().DownloadCompanyOverview();

        var downloadedFiles = Directory.GetFiles(ConfigurationReader.Test.TestDirectory);

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
        var insightsPage = HomeContext.Open().GoToInsights();
        var articleTitle = insightsPage
            .SwipeTheTopmostCarousel(times)
            .GetTopmostCarouselTitle();
        var isTitlePresentInTheArticle = insightsPage.GoToSlidelArticle().IsTitlePresent(articleTitle);

        Assert.That(isTitlePresentInTheArticle, Is.True);
    }
}
