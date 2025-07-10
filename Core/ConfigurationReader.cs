using Microsoft.Extensions.Configuration;

namespace Core;

public static class ConfigurationReader
{
    private static TestConfiguration? test;

    public static TestConfiguration Test
    {
        get
        {
            test ??= GetConfiguration();
            return test;
        }
    }

    private static TestConfiguration GetConfiguration()
    {
        var config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();

        return new TestConfiguration(config.GetSection("Test"));
    }
}