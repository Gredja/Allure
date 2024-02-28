using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Allure.Core.Web.Extensions;

public static class WebElementExtension
{
    public static bool IsStale(this IWebElement element)
    {
        try
        {
            var displayed = element.Displayed;
            return false;
        }
        catch (StaleElementReferenceException)
        {
            return true;
        }
    }

    public static bool HasClass(this IWebElement element, string className, int delayInSeconds = 0)
    {
        Thread.Sleep(TimeSpan.FromSeconds(delayInSeconds));

        var classes = element.GetAttribute("class");
        return classes.Split(' ').Any(@class => @class == className);
    }

    public static bool HasAttribute(this IWebElement element, string attributeName)
    {
        var attribute = element.GetAttribute(attributeName);
        return attribute != null;
    }

    public static DefaultWait<IWebElement> Wait(this IWebElement element, int timeoutInSec, int pollingIntervalInSec)
    {
        var defaultWait = new DefaultWait<IWebElement>(element)
        {
            Timeout = TimeSpan.FromSeconds(timeoutInSec),
            PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalInSec),
            Message = "Element not found."
        };

        defaultWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException),
            typeof(NullReferenceException), typeof(WebDriverException), typeof(WebDriverTimeoutException));

        return defaultWait;
    }

    public static IList<IWebElement> WaitForElementsExist(this IWebElement element, By by, int timeoutInSec, int pollingIntervalInSec)
    {
        try
        {
            Wait(element, timeoutInSec, pollingIntervalInSec).Until(x => x.FindElement(by));

            return element.FindElements(by);
        }
        catch (Exception ex)
        {
            return new List<IWebElement>();
        }
    }
}