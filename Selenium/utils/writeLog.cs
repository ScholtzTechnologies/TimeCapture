using TimeCapture.utils;

namespace TimeCapture.Selenium.utils
{
    public static class WriteLog
    {
        public static void write(string testName)
        {
            new _logger().Log(LogType.Info, testName);
        }
        public static void writePass(string Item, string TicketNo, string TimeType, string Description, string Type, string Date)
        {
            string Text = "[" + Item + "|" + TicketNo + "|" + TimeType + "|" + Description + "|" + Type + "|" + Date + "] : [Passed]";
            new _logger().Log(LogType.Info, Text);
        }
        public static void writeFail(string Item, string TicketNo, string TimeType, string Description, string Type, string Date)
        {
            string Text = "[" + Item + "|" + TicketNo + "|" + TimeType + "|" + Description + "|" + Type + "|" + Date + "] : [Failed]";
            new _logger().Log(LogType.Error, Text);
        }
    }
}
