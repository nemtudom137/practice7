using Microsoft.Extensions.Configuration;

namespace Core;

public class TestConfiguration
{
    private string? testDirectory;
    private int explicitTimeoutSec;

    public TestConfiguration(IConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(config);

        Url = config["Url"] ?? throw new ArgumentException(nameof(Url));

        if (!Enum.TryParse<BrowserType>(config["Browser"], out BrowserType browser))
        {
            throw new ArgumentException(nameof(Browser));
        }

        Browser = browser;

        if (!bool.TryParse(config["Headless"], out bool headless))
        {
            throw new ArgumentException(nameof(Headless));
        }

        Headless = headless;

        if (!int.TryParse(config["ExplicitTimeoutSec"], out int timeout))
        {
            throw new ArgumentException(nameof(ExplicitTimeoutSec));
        }

        ExplicitTimeoutSec = timeout;

        TestDirectory = config["TestDirectory"] ?? throw new ArgumentException(nameof(TestDirectory));
    }

    public string Url { get; init; }

    public BrowserType Browser { get; init; }

    public bool Headless { get; init; }

    public int ExplicitTimeoutSec
    {
        get => explicitTimeoutSec;
        init
        {
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, 0);
            explicitTimeoutSec = value;
        }
    }

    public string TestDirectory
    {
        get => testDirectory ?? throw new ArgumentException(nameof(TestDirectory));
        init
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(TestDirectory));
            }

            testDirectory = Path.IsPathRooted(value) ? value : Path.Combine(Directory.GetCurrentDirectory(), value);
        }
    }
}
