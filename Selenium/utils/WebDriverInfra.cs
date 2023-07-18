namespace TimeCapture.Selenium.utils
{
    internal static class WebDriverInfra
    {
        public static IWebDriver Create_Browser(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("headless");
                    chromeOptions.AcceptInsecureCertificates = true;
                    return new ChromeDriver(chromeOptions);

                default:
                    return (null);
            }
        }
    }
}
