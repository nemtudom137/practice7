using System.Collections.Concurrent;
using Core.UI.DriverFactory;
using OpenQA.Selenium;

namespace Core.UI;

public static class DriverContainer
{
    private static readonly ConcurrentDictionary<string, IWebDriver> Drivers = new ();

    public static void CloseDriver()
    {
        if (!Drivers.TryRemove(TestInfoHelper.GetTestName(), out IWebDriver? driver) || driver is null)
        {
            throw new InvalidOperationException("Missing valid driver instance");
        }

        driver.Quit();
    }

    public static void InitDriver()
    {
        var driver = DriverCreator.CreateDriver();

        if (!Drivers.TryAdd(TestInfoHelper.GetTestName(), driver))
        {
            throw new ArgumentException("Duplicated test case");
        }
    }

    public static IWebDriver GetDriver()
    {
        if (!Drivers.TryGetValue(TestInfoHelper.GetTestName(), out IWebDriver? driver) || driver is null)
        {
            throw new InvalidOperationException("Missing valid driver instance");
        }

        return driver;
    }
}
