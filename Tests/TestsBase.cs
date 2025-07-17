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
        ConfigurationManager.SetScreenshotFolder();
    }

    [SetUp]
    public void Setup()
    {
        Driver = DriverCreator.CreateDriver();
        ConfigurationManager.SetDownloadFolder();
        Log.Info($"{TestContext.CurrentContext.Test.MethodName} starts.");
    }

    [TearDown]
    public void TearDown()
    {
        var testName = TestContext.CurrentContext.Test.MethodName ?? "Unknown";
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;
        Log.Info($"{testName} ends with outcome {outcome}.");

        if (outcome == TestStatus.Failed)
        {
            var arguments = TestContext.CurrentContext.Test.Arguments;
            new ScreenshotMaker(Driver).TakeBrowserScreenshot(testName, arguments);
        }

        Driver?.Quit();
    }
}
