using BoDi;
using Core;
using Core.API;
using Core.UI;
using Core.UI.DriverFactory;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

[assembly: Parallelizable(ParallelScope.Fixtures)]

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

    [BeforeScenario("@UI")]
    public void BeforeUIScenario()
    {
        driver = DriverCreator.CreateDriver();
        objectContainer.RegisterInstanceAs(driver);
        FileHelper.SetScreenshotFolder();
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
        FileHelper.SetDownloadFolder();
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        LogHelper.Log.Info($"Test starts.");
    }

    [AfterScenario("@UI")]
    public void AfterUIScenario()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            new ScreenshotMaker(driver).TakeBrowserScreenshot();
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
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;
        LogHelper.Log.Info($"Test ends with outcome {outcome}.");
    }
}