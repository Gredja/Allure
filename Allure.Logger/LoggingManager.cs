using System.Collections.Concurrent;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using NUnit.Framework;

namespace Allure.Logger;

public static class LoggingManager
{
    private static readonly ConcurrentDictionary<NLog.Logger, string> Instances = new();
    private static readonly object SyncRoot = new();

    public static NLog.Logger Initialize()
    {
        var filepath = string.Empty;
        var loggerName = TestContext.CurrentContext.Test.FullName;
        LogManager.Configuration ??= new LoggingConfiguration();

        if (Instances.Keys.All(x => x.Name != loggerName))
        {
            lock (SyncRoot)
            {
                var fileTarget = LogManager.Configuration.FindTargetByName(loggerName);
                if (fileTarget == null)
                {
                    var newFileTarget = new FileTarget(loggerName);
                    filepath =
                        $"{TestContext.CurrentContext.TestDirectory}{Path.DirectorySeparatorChar}logs{Path.DirectorySeparatorChar}{loggerName}-{DateTime.Now:MMddyyHmmss}.log";
                    newFileTarget.FileName = filepath;
                    newFileTarget.Layout = new SimpleLayout(
                        "${longdate} ${uppercase:${level}} | ${logger} | PROCESSID ${processid} | THREADID ${threadid} | ${callsite} | ${message} ${newline} ${onexception:EXCEPTION OCCURRED\\:${exception:format=tostring}}");
                    LogManager.Configuration.AddTarget(newFileTarget);
                    var rule = new LoggingRule(loggerName, LogLevel.Trace, newFileTarget) { Final = true };
                    LogManager.Configuration.LoggingRules.Add(rule);
                    LogManager.ReconfigExistingLoggers();
                }
            }

            var localInstance = LogManager.GetLogger(loggerName);
            Instances.TryAdd(localInstance, filepath);
        }

        return GetInstance();
    }

    public static NLog.Logger GetInstance()
    {
        var instance = Instances.FirstOrDefault(x => x.Key.Name == TestContext.CurrentContext.Test.FullName).Key;

        return instance ?? throw new InvalidOperationException(
            $"You're trying to get logger instance for '{TestContext.CurrentContext.Test.FullName}', but it is not initialized. Call .Initialize()");
    }

    public static void CleanInstance()
    {
        var instance = Instances.Keys.FirstOrDefault(x => x.Name == TestContext.CurrentContext.Test.FullName);
        if (instance == null)
        {
            throw new InvalidOperationException(
                $"You're trying to remove logger instance for '{TestContext.CurrentContext.Test.FullName}', but it is not initialized. Call .Initialize()");
        }

        Instances.TryRemove(instance, out _);
    }

    public static void AttachResultsToTest()
    {
        var instance = Instances.Keys.FirstOrDefault(x => x.Name == TestContext.CurrentContext.Test.FullName);
        if (instance == null)
        {
            throw new InvalidOperationException(
                $"You're trying to remove logger instance for '{TestContext.CurrentContext.Test.FullName}', but it is not initialized. Call .Initialize()");
        }

        var filepath = Instances[instance];
        if (!string.IsNullOrWhiteSpace(filepath))
        {
            TestContext.AddTestAttachment(filepath);
        }
    }
}