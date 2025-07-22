using OpenQA.Selenium;

namespace Business.ApplicationInterface;

public class AboutPage : PageBase
{
    public static readonly By EpamAtAGlance = By.XPath("//span[contains(text()[last()],'a Glance')]/ancestor::div[@class='colctrl__holder']");
    public static readonly By DownloadButton = By.XPath("//span[contains(text(),'DOWNLOAD')]");

    internal AboutPage(IWebDriver driver)
        : base(driver, TimeSpan.FromSeconds(10))
    {
    }
}
