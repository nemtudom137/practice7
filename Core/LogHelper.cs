using System.Text;
using NLog;
using NLog.LayoutRenderers;

namespace Core;

[LayoutRenderer("testname")]
public class LogHelper : LayoutRenderer
{
    public static readonly Logger Log = LogManager.GetCurrentClassLogger();

    protected override void Append(StringBuilder builder, LogEventInfo logEvent)
    {
        string result = TestInfoHelper.GetTestName();
        builder.Append(result);
    }
}
