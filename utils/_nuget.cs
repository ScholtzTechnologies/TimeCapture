using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TimeCapture.utils
{
    public class _nuget
    {
        public bool UpdateChromeDriver(out string error)
        {
            try
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                error = "";
                return true;
            }
            catch (Exception exception)
            {
                error = exception.Message;
                return false;
            }
        }
    }
}
