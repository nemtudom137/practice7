using Business.API.ApplicationInterface;
using Business.API.ApplicationInterface.Models;
using RestSharp;

namespace Core.API;

public class UsersClient : BaseClient
{
    public UsersClient(IRequestBuilder builder)
        : base(builder)
    {
    }

    protected override string Resource => "users";

    public async Task<RestResponse<List<User>>> GetUsersAsync()
    {
        var request = Builder.AddResource(Resource)
            .SetMethod(Method.Get)
            .GetRequest();
        var response = await JsonPlaceholderClient.Client.ExecuteAsync<List<User>>(request);

        return response;
    }

    public async Task<RestResponse<User>> CreateUserAsync(User user)
    {
        var request = Builder.AddResource(Resource)
            .SetMethod(Method.Post)
            .AddJsonBody(user)
            .GetRequest();
        var response = await JsonPlaceholderClient.Client.ExecuteAsync<User>(request);

        return response;
    }
}