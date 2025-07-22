using Core.API;

namespace Business.API.ApplicationInterface;

public abstract class BaseClient
{
    protected BaseClient(IApiClient client, IRequestBuilder builder)
    {
        JsonPlaceholderClient = client;
        Builder = builder;
    }

    protected abstract string Resource { get; }

    protected IApiClient JsonPlaceholderClient { get; init; }

    protected IRequestBuilder Builder { get; init; }
}
