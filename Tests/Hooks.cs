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
        DriverContainer.InitDriver();
        FileHelper.SetScreenshotFolder();
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
        LogHelper.Log.Info($"{TestInfoHelper.GetTestName()} starts.");
    }

    [AfterScenario("@UI")]
    public void AfterUIScenario()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            ScreenshotMaker.TakeBrowserScreenshot();
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
        LogHelper.Log.Info($"{TestInfoHelper.GetTestName()} ends with outcome {outcome}.");
    }
}