namespace Core;

public class TestConfiguration
{
    public string Url { get; set; }

    public BrowserType Browser { get; set; }

    public bool Headless { get; set; }

    public int ExplicitTimeoutSec { get; set; }

    public string TestDirectory { get; set; }

    public string DownloadDirectory => Path.Combine(Directory.GetCurrentDirectory(), TestDirectory, "Download");

    public string ScreenshotDirectory => Path.Combine(Directory.GetCurrentDirectory(), TestDirectory, "Screenshot");


}
