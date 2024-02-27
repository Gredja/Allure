using System.Globalization;
using Allure.Driver.Base;
using Allure.Logger;
using Allure.Tests.ConfigModels;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Allure.Core.Tests.Base;

public abstract class BaseUiTest
{
    protected IWebDriver Driver;
    protected NLog.Logger Logger;
    protected string DefaultDownloadDirectory;
    protected BaseUiTestConfiguration BaseUiConfiguration;
    protected BaseDriverFactory DriverFactory;

    [SetUp]
    public void SetUpDriver()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        var uniquePart = Math.Abs(DateTime.Now.GetHashCode()).ToString();
        var executableFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        DefaultDownloadDirectory = Path.Combine(Path.GetDirectoryName(executableFilePath), "downloads",
            $"{TestContext.CurrentContext.Test.MethodName}_{uniquePart}");

        if (BaseUiConfiguration == null)
        {
            throw new Exception("Failed to read json config file. Please check the you initialized it!");
        }

        Driver = DriverFactory.InitBrowser(BaseUiConfiguration.BaseConfig.Browser, DefaultDownloadDirectory);
        Logger = LoggingManager.GetInstance();
        Logger.Info($"Start execution {TestContext.CurrentContext.Test.Name} test");
    }
}