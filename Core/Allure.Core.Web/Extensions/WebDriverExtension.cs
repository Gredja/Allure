using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Allure.Core.Web.Extensions;

public static class WebDriverExtension
{
    public static IWebElement WaitForElementExist(this IWebDriver driver, By by, int pollingIntervalInSec = 1,
        int timeoutInSec = 10)
    {
        Wait(driver, pollingIntervalInSec, timeoutInSec).Until(d => d.FindElements(by).Count > 0);

        return driver.FindElement(by);
    }

    public static WebDriverWait Wait(this IWebDriver driver, int pollingInterval, int timeoutInSec)
    {
        var webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSec))
        {
            PollingInterval = TimeSpan.FromSeconds(pollingInterval)
        };

        webDriverWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException),
            typeof(NullReferenceException), typeof(WebDriverException), typeof(WebDriverTimeoutException));

        return webDriverWait;
    }

    public static void WaitForElementNotExist(this IWebDriver driver, By by, int pollingIntervalInSec = 1, int timeoutInSec = 15)
    {
        try
        {
            Wait(driver, pollingIntervalInSec, timeoutInSec).Until(d => !HasWebElement(d, by));
        }
        catch
        {
            throw new NotFoundException("Timed out waiting for element to not exist with selector: " + by);
        }
    }

    public static IWebElement WaitForElementInteractable(this IWebDriver driver, By by, int pollingIntervalInSec = 1, int timeoutInSec = 10)
    {
        try
        {
            if (timeoutInSec > 0)
            {
                Wait(driver, pollingIntervalInSec, timeoutInSec).Until(d => d.FindElements(by).Count > 0 && d.FindElement(by).Enabled);
            }
        }
        catch (Exception e)
        {
            throw new Exception($"{timeoutInSec} secs timed out waiting for element to be visible with selector: {by}. {e.Message}");
        }

        return driver.FindElement(by);
    }

    public static bool WaitForElementNotDisplayed(this IWebDriver driver, By by, int timeoutInSeconds = 5, int iterationDelay = 1)
    {
        try
        {
            Wait(driver, timeoutInSeconds, iterationDelay)
                .Until(d => d.FindElements(by).Count > 0 || !d.FindElement(by).Displayed);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool WaitForJsReady(this IWebDriver driver, int pollingIntervalInSec = 1, int timeoutInSec = 10)
    {
        try
        {
            return Wait(driver, pollingIntervalInSec, timeoutInSec).Until(x =>
                ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public static bool IsElementDisplayed(this IWebDriver driver, By by, int iterationDelay = 1, int timeoutInSeconds = 5)
    {
        try
        {
            Wait(driver, iterationDelay, timeoutInSeconds)
                .Until(d => d.FindElements(by).Count > 0 && d.FindElement(by).Displayed);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool HasWebElement(this IWebDriver driver, By by, int timeoutInSeconds = 0)
    {
        try
        {
            WaitForElementExist(driver, by, 1, timeoutInSeconds);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static void AcceptAlertConfirmation(this IWebDriver driver)
    {
        try
        {
            driver.SwitchTo().Alert().Accept();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}