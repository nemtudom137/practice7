using System.Collections.Concurrent;

namespace Core.API;

public static class ApiClientContainer
{
    private static readonly ConcurrentDictionary<string, IApiClient> Drivers = new ();

    public static void CloseClient()
    {
        if (!Drivers.TryRemove(TestInfoHelper.GetTestName(), out IApiClient? client) || client is null)
        {
            throw new InvalidOperationException("Missing valid driver instance");
        }

        client.Dispose();
    }

    public static void InitClient(IApiClient client)
    {
        ArgumentNullException.ThrowIfNull(client);

        if (!Drivers.TryAdd(TestInfoHelper.GetTestName(), client))
        {
            throw new ArgumentException("Duplicated test case");
        }
    }

    public static IApiClient GetClient()
    {
        if (!Drivers.TryGetValue(TestInfoHelper.GetTestName(), out IApiClient? client) || client is null)
        {
            throw new InvalidOperationException("Missing valid client instance");
        }

        return client;
    }
}
