using System.Text.Json;
using System.Text.Json.Serialization;
using Core;
using RestSharp;
using RestSharp.Serializers.Json;

namespace Core.API;

public class JsonPlaceholderClient : IApiClient
{
    public JsonPlaceholderClient()
    {
        var serializerOptions = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

        Client = new RestClient(
            options: new RestClientOptions() { BaseUrl = new Uri(ConfigurationManager.API.Url) },
            configureSerialization: s => s.UseSystemTextJson(serializerOptions));
    }

    public IRestClient Client { get; init; }

    void IDisposable.Dispose()
    {
        Client?.Dispose();
        GC.SuppressFinalize(this);
    }
}
