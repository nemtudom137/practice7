using Business.ApplicationInterface;
using OpenQA.Selenium;

namespace Business.Business;

public class JobDetailContext : ContextBase
{
    private readonly JobDetailPage page;

    public JobDetailContext(IWebDriver? driver)
        : base(driver)
    {
        page = new JobDetailPage(Driver);
    }

    public bool IsLanguagePresent(string language)
    {
        return page.GetNumberOfOccurances(language) > 0;
    }
}