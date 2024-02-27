using Allure.Driver.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Allure.Driver.Base;

public abstract class BaseDriverFactory
{
    public IWebDriver InitBrowser(BrowserType browser, string defaultDownloadDirectory)
    {
        try
        {
            IWebDriver? webDriver;

            switch (browser)
            {
                case BrowserType.Chrome:
                    webDriver = CreateChromeDriver(defaultDownloadDirectory);
                    break;
                default:
                    webDriver = CreateChromeDriver(defaultDownloadDirectory);
                    break;
            }

            return webDriver;
        }
        catch (Exception innerException)
        {
            throw new Exception("Failed to create browser instance", innerException);
        }
    }

    protected ChromeDriver CreateChromeDriver(ChromeOptions chromeOptions)
    {
        var driversLocation = new DriverManager().SetUpDriver(new ChromeConfig());
        var chromeDriver = new ChromeDriver(GetChromeDriverService(driversLocation), chromeOptions, TimeSpan.FromMinutes(2));

        chromeDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
        chromeDriver.Manage().Window.Maximize();

        return chromeDriver;
    }

    protected virtual ChromeDriver CreateChromeDriver(string defaultDownloadDirectory)
    {
        var chromeOptions = CreateDefaultChromeOptions(defaultDownloadDirectory);
        return CreateChromeDriver(chromeOptions);
    }

    protected ChromeOptions CreateDefaultChromeOptions(string defaultDownloadDirectory)
    {
        var chromeOptions = new ChromeOptions
        {
            LeaveBrowserRunning = false
        };
        chromeOptions.AddArgument("test-type");
        chromeOptions.AddUserProfilePreference("profile.default_content_settings.popups", 0);
        chromeOptions.AddUserProfilePreference("download.prompt_for_download", "false");
        chromeOptions.AddUserProfilePreference("download.default_directory", defaultDownloadDirectory);

        return chromeOptions;
    }

    private ChromeDriverService GetChromeDriverService(string driversLocation)
    {
        var chromeDriverService = ChromeDriverService.CreateDefaultService(driversLocation);
        chromeDriverService.EnableVerboseLogging = true;

        return chromeDriverService;
    }
}