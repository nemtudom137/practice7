using NUnit.Framework;

namespace Core;

public static class TestInfoHelper
{
    public static string GetTestName()
    {
        var test = TestContext.CurrentContext.Test.MethodName ?? "Unknown";
        var arguments = TestContext.CurrentContext.Test.Arguments;
        string testName;
        if (arguments is null || !arguments.Any(x => x is not null))
        {
            testName = test;
        }
        else
        {
            string argString = string.Join("_", arguments.Where(x => x is not null));
            testName = $"{test}_{argString}";
        }

        return testName;
    }
}
