using Microsoft.Extensions.Configuration;

namespace Core;

public class TestConfiguration
{
    private readonly string testDirectory;

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

        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(timeout, 0);
        ExplicitTimeoutSec = timeout;

        var directory = config["TestDirectory"];
        if (string.IsNullOrWhiteSpace(directory))
        {
            throw new ArgumentException(nameof(DirectoryForDownload));
        }

        testDirectory = Path.IsPathRooted(directory) ? directory : Path.Combine(Directory.GetCurrentDirectory(), directory);
    }

    public string Url { get; init; }

    public BrowserType Browser { get; init; }

    public bool Headless { get; init; }

    public int ExplicitTimeoutSec { get; init; }

    public string DirectoryForDownload => Path.Combine(testDirectory, "Download");

    public string DirectoryForScreenshots => Path.Combine(testDirectory, "Screenshots");
}
