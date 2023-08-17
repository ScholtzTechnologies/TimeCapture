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
                    ChromeDriverService chromeService = ChromeDriverService.CreateDefaultService();
                    chromeService.SuppressInitialDiagnosticInformation = true;
                    chromeService.HideCommandPromptWindow = true;
                    chromeOptions.AddArgument("headless");
                    chromeOptions.AddArgument("silent");
                    chromeOptions.AcceptInsecureCertificates = true;
                    return new ChromeDriver(chromeService, chromeOptions);

                default:
                    return (null);
            }
        }
    }
}
