using Core.API;
using RestSharp;

namespace Business.API.ApplicationInterface;

public class InvalidEndpointClient : BaseClient
{
    public InvalidEndpointClient(IApiClient client, IRequestBuilder builder)
        : base(client, builder)
    {
    }

    protected override string Resource => "invalidendpoint";

    public async Task<RestResponse> GetAsync()
    {
        var request = Builder.AddResource(Resource)
            .SetMethod(Method.Get)
            .GetRequest();
        var response = await JsonPlaceholderClient.Client.ExecuteAsync(request);

        return response;
    }
}
