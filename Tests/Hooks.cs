using BoDi;
using Core;
using Core.DriverFactory;
using NLog;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public sealed class Hooks
{
    public static readonly Logger Log = LogManager.GetCurrentClassLogger();
    private readonly IObjectContainer objectContainer;
    private IWebDriver? driver;

    public Hooks(IObjectContainer objectContainer)
    {
        this.objectContainer = objectContainer;
    }

    [BeforeScenario(Order = 0)]
    public void BeforeScenario()
    {
        driver = DriverCreator.CreateDriver();
        objectContainer.RegisterInstanceAs(driver);
        ConfigurationManager.SetScreenshotFolder();
        Log.Info($"{TestContext.CurrentContext.Test.MethodName} starts.");
    }

    [BeforeScenario("@download")]
    public void BeforeScenarioWithDownload()
    {
        ConfigurationManager.SetDownloadFolder();
    }

    [AfterScenario]
    public void AfterScenario()
    {
        var testName = TestContext.CurrentContext.Test.MethodName ?? "Unknown";
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;
        Log.Info($"{testName} ends with outcome {outcome}.");

        if (outcome == TestStatus.Failed)
        {
            var arguments = TestContext.CurrentContext.Test.Arguments;
            new ScreenshotMaker(driver).TakeBrowserScreenshot(testName, arguments);
        }

        driver?.Quit();
    }
}