namespace Core;

public static class FileHelper
{
    public static void SetDownloadFolder()
    {
        var download = ConfigurationManager.UI.DownloadDirectory;
        if (Directory.Exists(download))
        {
            Directory.Delete(download, true);
        }

        Directory.CreateDirectory(download);
    }

    public static void SetScreenshotFolder()
    {
        var screenshots = ConfigurationManager.UI.ScreenshotDirectory;

        if (!Directory.Exists(screenshots))
        {
            Directory.CreateDirectory(screenshots);
        }
    }
}