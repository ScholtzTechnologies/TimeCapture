using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TimeCapture.Selenium.utils
{
    public static class DriverExtensions
    {
        public static void ClearAndSendKeys(this IWebElement element, string Text)
        {
            try
            {
                element.Clear();
                element.SendKeys(Text);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public static void JSClick(this IWebElement element, IJavaScriptExecutor js)
        {
            js.ExecuteScript("arguments[0].click()", element);
        }

        public static void SpinnerHidden(this WebDriverWait wait, IWebDriver driver)
        {
            try
            {
                if (driver.FindElement(By.XPath("//div[contains(@class, 'spinnerOverlay')]")).Displayed)
                {
                    wait.Until(driver => !driver.FindElement(By.XPath("//div[contains(@class, 'spinnerOverlay')]")).Displayed);
                }   
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("Timed out after 10 seconds"))
                {
                    WriteLog.write("    Spinner took longer than 10 seconds");
                    if (driver.FindElement(By.XPath("//div[contains(@class, 'spinnerOverlay')]")).Displayed)
                    {
                        wait.Until(driver => !driver.FindElement(By.XPath("//div[contains(@class, 'spinnerOverlay')]")).Displayed);
                    }
                }
                else
                    throw;
            }
        }

        public static bool IsElementPresent(this IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by).Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}
