using TimeCapture.DB;
using Uno.Extensions.Specialized;

namespace TimeCapture.Selenium.TimeTaker
{
    public class TimeCapture
    {
        public string Username = new Access().GetUserName();
        public string Password = new Access().GetPassword();
        public void CaptureTime(BrowserType browserType)
        {
            IJavaScriptExecutor js;
            using (var driver = WebDriverInfra.Create_Browser(browserType))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                js = (IJavaScriptExecutor)driver;

                driver.Navigate().GoToUrl("https://web3.searchlightsoftware.co.za/Tickets/Login.aspx");
                driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);

                CSVImport.CreateFolder();

                string root = Directory.GetCurrentDirectory();
                string filePath = Path.Combine(root, "csvImport", "ZZZImport_CSV.csv");

                driver.FindElement(By.Id("UserName")).SendKeys(Username);
                driver.FindElement(By.Id("Password")).SendKeys(Password);
                js.ExecuteScript("arguments[0].click()", driver.FindElement(By.XPath("/html/body/form/div[3]/div/div[2]/input")));

                driver.FindElement(By.XPath("/html/body/form/div[3]/div/a[2]")).Click();


                DataSet records = new Access().GetTimeToCapture();
                if (records.HasRows())
                    foreach (DataRow time in records.Tables[0].Rows)
                    {
                        string sDate = time.GetDataRowStringValue("Date");
                        try
                        {
                            driver.FindElement(By.XPath("//div[1]/div[1]/div/div/div[3]/div[1]/input")).Clear();
                            driver.FindElement(By.XPath("//div[1]/div[1]/div/div/div[3]/div[1]/input")).SendKeys(time.GetDataRowStringValue("TicketNo"));
                            js.ExecuteScript("arguments[0].click()", driver.FindElement(By.XPath("//div[1]/div/div/div[3]/div[4]/a")));
                            Thread.Sleep(1000);

                            js.ExecuteScript("arguments[0].click()", driver.FindElement(By.XPath("//tbody/tr/td/table[1]/tbody/tr[3]/td[1]/a[3]")));
                            Thread.Sleep(1000);

                            int TimeType = 4;
                            if (time.GetDataRowStringValue("TicketType").Contains("Non-Chargeable") || time.GetDataRowStringValue("TicketType").Contains("Non-Chargable"))
                            {
                                TimeType = 3;
                            }
                            else if (time.GetDataRowStringValue("TicketType").Contains("Chargeable") || time.GetDataRowStringValue("TicketType").Contains("Chargable"))
                            {
                                TimeType = 1;
                            }
                            else if (time.GetDataRowStringValue("TicketType").Contains("Support Contract") || time.GetDataRowStringValue("TicketType").Contains("Support"))
                            {
                                TimeType = 2;
                            }

                            driver.FindElement(By.XPath("//div[3]/div[2]/div/div[2]/select/option[" + TimeType + "]")).Click();

                            driver.FindElement(By.XPath("//div[3]/div[2]/div/div[1]/input")).SendKeys(time.GetDataRowStringValue("Item"));

                            if (time.GetDataRowStringValue("Description").Length > 0)
                            {
                                driver.FindElement(By.XPath("//div[2]/div/div/div[3]/div[3]/input")).SendKeys(time.GetDataRowStringValue("Description"));
                            }

                            driver.FindElement(By.XPath("//div[3]/div[4]/div/div[1]/input")).Clear();
                            driver.FindElement(By.XPath("//div[3]/div[4]/div/div[1]/input")).SendKeys(sDate + " " + time.GetDataRowStringValue("Start"));
                            driver.FindElement(By.XPath("//div[3]/div[4]/div/div[2]/input")).Clear();
                            driver.FindElement(By.XPath("//div[3]/div[4]/div/div[2]/input")).SendKeys(sDate + " " + time.GetDataRowStringValue("End"));

                            int Type = 1;
                            if (time.GetDataRowStringValue("TimeType").Contains("General"))
                            {
                                Type = 1;
                            }
                            else if (time.GetDataRowStringValue("TimeType").Contains("Investigation"))
                            {
                                Type = 2;
                            }
                            else if (time.GetDataRowStringValue("TimeType").Contains("Report"))
                            {
                                Type = 3;
                            }
                            else if (time.GetDataRowStringValue("TimeType").Contains("Bug"))
                            {
                                Type = 4;
                            }
                            else if (time.GetDataRowStringValue("TimeType").Contains("Dev") || time.GetDataRowStringValue("TimeType").Contains("Develop") || time.GetDataRowStringValue("TimeType").Contains("Development"))
                            {
                                Type = 5;
                            }
                            else if (time.GetDataRowStringValue("TimeType").Contains("Meeting") || time.GetDataRowStringValue("TimeType").Contains("Meetings"))
                            {
                                Type = 10;
                            }
                            else if (time.GetDataRowStringValue("TimeType").Contains("Training"))
                            {
                                Type = 12;
                            }
                            else if (time.GetDataRowStringValue("TimeType").Contains("Testing") || time.GetDataRowStringValue("TimeType").Contains("Test"))
                            {
                                Type = 13;
                            }

                            driver.FindElement(By.XPath("//div[3]/div[5]/div/div[2]/select/option[" + Type + "]")).Click();
                            var saveBtn = driver.FindElement(By.XPath("//div[2]/div/div/div[4]/a[1]"));
                            js.ExecuteScript("arguments[0].click()", saveBtn);
                            new Access().MarkTimeAsCaptured(time.GetDataRowIntValue("TimeID"));
                            WriteLog.writePass(time.GetDataRowStringValue("Item"), time.GetDataRowStringValue("TicketNo"), time.GetDataRowStringValue("TimeType"), time.GetDataRowStringValue("Description"), time.GetDataRowStringValue("TicketType"), sDate);
                            Thread.Sleep(2000);
                        }
                        catch (Exception ex)
                        {
                            WriteLog.writeFail(time.GetDataRowStringValue("Item"), time.GetDataRowStringValue("TicketNo"), time.GetDataRowStringValue("TimeType"), time.GetDataRowStringValue("Description"), time.GetDataRowStringValue("TicketType"), sDate);
                        }
                    }
                    driver.Quit();
            }
        }
    }

    public class TicketSystem
    {
        public string Username = new Access().GetUserName();
        public string Password = new Access().GetPassword();
        public void GetTicketURL(BrowserType browserType, string ticketNo, out string ticketURL)
        {
            IJavaScriptExecutor js;
            using (var driver = WebDriverInfra.Create_Browser(browserType))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                js = (IJavaScriptExecutor)driver;

                driver.Navigate().GoToUrl("https://web3.searchlightsoftware.co.za/Tickets/Login.aspx");
                driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);

                driver.FindElement(By.Id("UserName")).SendKeys(Username);
                driver.FindElement(By.Id("Password")).SendKeys(Password);
                js.ExecuteScript("arguments[0].click()", driver.FindElement(By.XPath("/html/body/form/div[3]/div/div[2]/input")));

                driver.FindElement(By.XPath("/html/body/form/div[3]/div/a[2]")).Click();

                driver.FindElement(By.XPath("//div[1]/div[1]/div/div/div[3]/div[1]/input")).Clear();
                driver.FindElement(By.XPath("//div[1]/div[1]/div/div/div[3]/div[1]/input")).SendKeys(ticketNo);
                js.ExecuteScript("arguments[0].click()", driver.FindElement(By.XPath("//div[1]/div/div/div[3]/div[4]/a")));
                Thread.Sleep(1000);
                try
                {
                    js.ExecuteScript("arguments[0].click()", driver.FindElement(By.XPath("//tr[3]/td[1]/a[1]")));
                    Thread.Sleep(1000);
                    ticketURL = driver.Url;
                }
                catch
                {
                    MessageBox.Show("No ticket found");
                    ticketURL = null;
                }
                driver.Quit();
            }
        }
    }
}