using RestSharp;

namespace Core.API;

public class RequestBuilder : IRequestBuilder
{
    private RestRequest request;

    public RequestBuilder()
    {
        request = new RestRequest();
    }

    public IRequestBuilder SetMethod(Method method)
    {
        request.Method = method;
        return this;
    }

    public IRequestBuilder AddResource(string resource)
    {
        request.Resource = resource;
        return this;
    }

    public IRequestBuilder AddHeader(string name, string value)
    {
        request.AddHeader(name, value);
        return this;
    }

    public IRequestBuilder AddQueryParameter(string name, string value)
    {
        request.AddQueryParameter(name, value);
        return this;
    }

    public IRequestBuilder AddJsonBody(object body)
    {
        request.AddJsonBody(body);
        return this;
    }

    public IRequestBuilder Reset()
    {
        request = new RestRequest();
        return this;
    }

    public RestRequest GetRequest() => request;
}