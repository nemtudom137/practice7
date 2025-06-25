using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace WebApp.Pages;

public class AboutPage : BasePage
{
    private readonly By EPAMAtAGlance = By.XPath("//span[contains(text()[last()],'a Glance')]/ancestor::div[@class='colctrl__holder']");
    private readonly By downloadButton = By.XPath("//span[contains(text(),'DOWNLOAD')]");

    internal AboutPage(IWebDriver driver) : base(driver, TimeSpan.FromSeconds(10))
    {
    }

    public AboutPage ScrollToEPAMAtAGlance()
    {
        var section = Driver.FindElement(EPAMAtAGlance);
        new Actions(Driver).ScrollToElement(section).Perform();
        return this;
    }

    public void ClickOnDownload(string directory)
    {
        Driver.FindElement(downloadButton).Click();
        WaitHelp.WaitForDowloadedFile(directory);
    }
}
