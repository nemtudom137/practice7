using Core;
using Core.DriverFactory;
using NLog;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace Tests;

public class TestsBase
{
    public static readonly Logger Log = LogManager.GetCurrentClassLogger();

    protected IWebDriver? Driver { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        FileHelper.SetScreenshotFolder();
    }

    [SetUp]
    public void Setup()
    {
        Driver = DriverCreator.CreateDriver();
        FileHelper.SetDownloadFolder();
        Log.Info($"Test starts.");
    }

    [TearDown]
    public void TearDown()
    {
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;
        Log.Info($"Test ends with outcome {outcome}.");

        if (outcome == TestStatus.Failed)
        {
            new ScreenshotMaker(Driver).TakeBrowserScreenshot();
        }

        Driver?.Quit();
    }
}
