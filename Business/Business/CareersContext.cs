using Business.ApplicationInterface;

namespace Business.Business;

public class CareersContext
{
    private readonly CareersPage page;

    internal CareersContext()
    {
        page = new CareersPage();
    }

    public CareersContext SetSearchTerms(string searchTerms)
    {
        page.SetField(CareersPage.Keyword, searchTerms);
        return this;
    }

    public CareersContext SetLocation(string location, string city)
    {
        if (string.IsNullOrEmpty(city))
        {
            page.SetLocation(location);
        }
        else
        {
            page.SetLocation(location, city);
        }

        return this;
    }

    public CareersContext ChooseRemote()
    {
        page.ChooseRemote();
        return this;
    }

    public JobListingsContext ClickOnFind()
    {
        PageBase.Click(CareersPage.FindButton);
        return new JobListingsContext();
    }
}
