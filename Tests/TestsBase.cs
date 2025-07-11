using Core;
using NUnit.Framework.Interfaces;

namespace Tests;

public class TestsBase
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        ConfigurationManager.SetScreenshotFolder();
    }

    [SetUp]
    public void Setup()
    {
        ConfigurationManager.SetDownloadFolder();
        LogHelper.Info($"{TestContext.CurrentContext.Test.MethodName} starts.");
    }

    [TearDown]
    public void TearDown()
    {
        var testName = TestContext.CurrentContext.Test.MethodName ?? "Unknown";
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;
        LogHelper.Info($"{testName} ends with outcome {outcome}.");

        if (outcome == TestStatus.Failed)
        {
            var arguments = TestContext.CurrentContext.Test.Arguments;
            ScreenshotMaker.TakeBrowserScreenshot(testName, arguments);
        }

        DriverContainer.QuitDriver();
    }
}
