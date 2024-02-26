using Allure.Driver.Enums;
using OpenQA.Selenium;

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
}