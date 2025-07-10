using OpenQA.Selenium;

namespace Core.DriverFactory;

internal interface IDriverFactory
{
    IWebDriver CreateDriver(bool headless, string downloadDirectory);
}
