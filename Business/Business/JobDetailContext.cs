using Business.ApplicationInterface;

namespace Business.Business;

public class JobDetailContext
{
    private readonly JobDetailPage page;

    public JobDetailContext()
    {
        page = new JobDetailPage();
    }

    public bool IsLanguagePresent(string language)
    {
        return page.GetNumberOfOccurances(language) > 0;
    }
}