using Allure.Driver.Base;
using OpenQA.Selenium.Chrome;

namespace Allure.Driver;

public class OnSiteDriverFactory : BaseDriverFactory
{
    protected override ChromeDriver CreateChromeDriver(string defaultDownloadDirectory)
    {
        var chromeOptions = CreateDefaultChromeOptions(defaultDownloadDirectory);

        return CreateChromeDriver(chromeOptions);
    }
}