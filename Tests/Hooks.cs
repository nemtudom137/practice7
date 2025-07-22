using BoDi;
using Core;
using Core.API;
using Core.UI;
using Core.UI.DriverFactory;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public sealed class Hooks
{
    private readonly IObjectContainer objectContainer;
    private IWebDriver? driver;
    private IApiClient? client;

    public Hooks(IObjectContainer objectContainer)
    {
        this.objectContainer = objectContainer;
    }

    [BeforeScenario("UI")]
    public void BeforeUIScenario()
    {
        driver = DriverCreator.CreateDriver();
        objectContainer.RegisterInstanceAs(driver);
        ConfigurationManager.SetScreenshotFolder();
    }

    [BeforeScenario("@API")]
    public void BeforeAPIScenario()
    {
        client = new JsonPlaceholderClient();
        objectContainer.RegisterInstanceAs(client);
        objectContainer.RegisterInstanceAs((IRequestBuilder)new RequestBuilder());
    }

    [BeforeScenario("@download")]
    public void BeforeScenarioWithDownload()
    {
        ConfigurationManager.SetDownloadFolder();
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        LogHelper.Log.Info($"{TestContext.CurrentContext.Test.MethodName} starts.");
    }

    [AfterScenario("@UI")]
    public void AfterUIScenario()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            var arguments = TestContext.CurrentContext.Test.Arguments;
            var testName = TestContext.CurrentContext.Test.MethodName ?? "Unknown";
            new ScreenshotMaker(driver).TakeBrowserScreenshot(testName, arguments);
        }

        driver?.Quit();
    }

    [AfterScenario("@API")]
    public void AfterAPIScenario()
    {
        client?.Dispose();
    }

    [AfterScenario]
    public void AfterScenario()
    {
        var testName = TestContext.CurrentContext.Test.MethodName ?? "Unknown";
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;
        LogHelper.Log.Info($"{testName} ends with outcome {outcome}.");
    }
}