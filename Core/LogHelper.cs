using NLog;

namespace Core;

public static class LogHelper
{
    public static readonly Logger Log = LogManager.GetCurrentClassLogger();
}
