using System.Globalization;
using Allure.Core.Web.Tests.Config;
using Allure.Driver.Base;
using Allure.Logger;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace Allure.Core.Web.Tests.Base;

public abstract class BaseWebTest
{
    protected IWebDriver Driver;
    protected NLog.Logger Logger;
    protected string DefaultDownloadDirectory;
    protected WebTestConfiguration WebConfiguration;
    protected BaseDriverFactory DriverFactory;

    [SetUp]
    public void SetUpDriver()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        var uniquePart = Math.Abs(DateTime.Now.GetHashCode()).ToString();
        var executableFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        DefaultDownloadDirectory = Path.Combine(Path.GetDirectoryName(executableFilePath), "downloads",
            $"{TestContext.CurrentContext.Test.MethodName}_{uniquePart}");

        if (WebConfiguration == null)
        {
            throw new Exception("Failed to read json config file. Please check the you initialized it!");
        }

        Driver = DriverFactory.InitBrowser(WebConfiguration.Browser, DefaultDownloadDirectory);
        Logger = LoggingManager.Initialize();
        Logger.Info($"Start execution {TestContext.CurrentContext.Test.Name} test");
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            if (Driver != null)
            {
                if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Passed)
                {
                    TakeScreenshot();
                }

                Driver.Quit();
                Driver.Dispose();
                Driver = null;
            }
        }
        catch (Exception e)
        {
            Logger.Warn(e, "Unable to close browser");
        }

        Logger.Info($"Finish execution {TestContext.CurrentContext.Test.Name} test");
        LoggingManager.AttachResultsToTest();
        LoggingManager.CleanInstance();
    }

    private void TakeScreenshot()
    {
        try
        {
            var filename =
                @$"{TestContext.CurrentContext.TestDirectory}{Path.DirectorySeparatorChar}{TestContext.CurrentContext.Test.FullName}_[{DateTime.Now:yyyy.dd.M--HH-mm-ss}].png";
            ((ITakesScreenshot)Driver).GetScreenshot().SaveAsFile(filename);
            TestContext.AddTestAttachment(filename);
        }
        catch (Exception screenshotTakerException)
        {
            Logger.Warn($"Can not save screenshot. Message: {screenshotTakerException.Message}");
        }
    }
}