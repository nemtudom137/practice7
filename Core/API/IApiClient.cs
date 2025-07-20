using RestSharp;

namespace Core.API;

public interface IApiClient : IDisposable
{
    IRestClient Client { get; init; }
}