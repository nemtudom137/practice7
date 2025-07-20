using OpenQA.Selenium;

namespace Core.UI.DriverFactory;

internal interface IDriverFactory
{
    IWebDriver CreateDriver(bool headless, string downloadDirectory);
}
