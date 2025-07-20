using RestSharp;

namespace Core.API;

public interface IRequestBuilder
{
    IRequestBuilder SetMethod(Method method);

    IRequestBuilder AddHeader(string name, string value);

    IRequestBuilder AddQueryParameter(string name, string value);

    IRequestBuilder AddResource(string resource);

    IRequestBuilder AddJsonBody(object body);

    IRequestBuilder Reset();

    RestRequest GetRequest();
}