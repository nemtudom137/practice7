using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebApp.Pages;

namespace WebAppTests;

[TestFixture(true)]
[TestFixture(false)]
public class Tests
{
    private static readonly object[][] CarrierSearch =
    [
        ["C", "All Locations", string.Empty],
        ["Java", "Japan", "Tokyo"]
    ];

    private static readonly object[] GlobalSearch =
    [
       "BLOCKCHAIN",
       "RPA"
    ];

    private static readonly object[] SwipeTimes = [0, 2, 7];

    private readonly string downloadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "DownloadTest");
    private readonly bool headlessMode;
    private IWebDriver? driver;

    public Tests(bool headlessMode)
    {
        this.headlessMode = headlessMode;
    }

    [SetUp]
    public void Setup()
    {
        if (Directory.Exists(downloadDirectory))
        {
            Directory.Delete(downloadDirectory, true);
        }

        Directory.CreateDirectory(downloadDirectory);

        var options = new ChromeOptions();
        options.AddUserProfilePreference("download.default_directory", downloadDirectory);
        if (headlessMode)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
        }
        else
        {
            options.AddArgument("--start-maximized");
        }

        driver = new ChromeDriver(options);
    }

    [TearDown]
    public void TearDown()
    {
        driver?.Quit();
        Directory.Delete(downloadDirectory, true);
    }

    [Test]
    [TestCaseSource(nameof(CarrierSearch))]
    public void CarrierSearchResult_ContainsLanguage(string language, string location, string city)
    {
        var header = new HomePage(driver)
            .AcceptCookies()
            .Header;

        var n = header.ClickOnCareers()
            .SetSearchTerms(language)
            .SetLocation(location, city)
            .ChooseRemote()
            .ClickOnFind()
            .SortResultByDate()
            .ClicOnTheLastApplyButton()
            .GetNumberOfOccurances(language);

        Assert.That(n, Is.GreaterThan(0));
    }

    [Test]
    [TestCaseSource(nameof(GlobalSearch))]
    public void GlobalSearchFirstResults_ContainKeyword(string searchString)
    {
        var header = new HomePage(driver)
            .AcceptCookies()
            .Header;

        var results = header.ClickOnSearchIcon()
            .SetSearchTerms(searchString)
            .ClickOnFind()
            .GetSearchResults();

        Assert.That(results.Count > 0 && results.All(x => x.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)), Is.True);
    }

    [Test]
    [TestCase("EPAM_Corporate_Overview_Q4FY-2024.pdf")]
    public void DownloadOnAboutPage_GivesFileWithCorrectName(string expectedFileName)
    {
        var header = new HomePage(driver)
            .AcceptCookies()
            .Header;

        header.ClickOnAbout()
             .ScrollToEPAMAtAGlance()
             .ClickOnDownload(downloadDirectory);

        var downloadedFiles = Directory.GetFiles(downloadDirectory);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(downloadedFiles.Length, Is.EqualTo(1));
            Assert.That(Path.GetFileName(downloadedFiles[0]), Is.EqualTo(expectedFileName));
        }
    }

    [Test]
    [TestCaseSource(nameof(SwipeTimes))]
    public void CarouselOnInsightsPage_RedirectToArticleWithCorrectName(int times)
    {
        var header = new HomePage(driver)
            .AcceptCookies()
            .Header;

        var carusel = header.ClickOnInsights()
            .TopmostCarousel;

        var articleTitle = carusel.Swipe(times)
            .GetActivArticleTitle();

        Console.WriteLine(articleTitle);

        var isTitlePresentInTheArticle = carusel.ClickOnReadMore()
            .GetNumberOfOccurances(articleTitle);

        Assert.That(isTitlePresentInTheArticle, Is.True);
    }
}
