using Core.DriverFactory;
using OpenQA.Selenium;

namespace Core;

public static class DriverContainer
{
    private static IWebDriver? driver;

    public static IWebDriver Driver
    {
        get
        {
            driver ??= DriverCreator.CreateDriver();
            return driver;
        }
    }

    public static void QuitDriver()
    {
        driver?.Quit();
        driver = null;
        LogHelper.Info($"Driver is closed.");
    }
}
