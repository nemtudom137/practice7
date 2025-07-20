using BoDi;
using Core;
using Core.API;
using Core.UI;
using NUnit.Framework.Interfaces;
using TechTalk.SpecFlow;

[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace Tests;

[Binding]
public sealed class Hooks
{
    private readonly IObjectContainer objectContainer;

    public Hooks(IObjectContainer objectContainer)
    {
        this.objectContainer = objectContainer;
    }

    [BeforeScenario("UI")]
    public void BeforeUIScenario()
    {
        driver = DriverCreator.CreateDriver();
        objectContainer.RegisterInstanceAs(driver);
        FileHelper.SetScreenshotFolder();
        LogHelper.Log.Info($"{TestContext.CurrentContext.Test.MethodName} starts.");
    }

    [BeforeScenario("@API")]
    public void BeforeAPIScenario()
    {
        ApiClientContainer.InitClient(new JsonPlaceholderClient());
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

        DriverContainer.CloseDriver();
    }

    [AfterScenario("@API")]
    public void AfterAPIScenario()
    {
        ApiClientContainer.CloseClient();
    }

    [AfterScenario]
    public void AfterScenario()
    {
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;
        LogHelper.Log.Info($"Test ends with outcome {outcome}.");
    }
}