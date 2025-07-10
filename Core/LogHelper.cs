using NLog;

namespace Core;

public static class LogHelper
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public static void Trace(string msg) => Log.Trace(msg);

    public static void Debug(string msg) => Log.Debug(msg);

    public static void Info(string msg) => Log.Info(msg);

    public static void Warn(string msg) => Log.Warn(msg);

    public static void Error(string msg) => Log.Error(msg);

    public static void Fatal(string msg) => Log.Fatal(msg);
}
