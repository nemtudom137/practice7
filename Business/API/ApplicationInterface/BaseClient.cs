using Core.API;

namespace Business.API.ApplicationInterface;

public abstract class BaseClient
{
    protected BaseClient(IRequestBuilder builder)
    {
        Builder = builder;
    }

    protected static IApiClient JsonPlaceholderClient => ApiClientContainer.GetClient();

    protected abstract string Resource { get; }

    protected IRequestBuilder Builder { get; init; }
}
