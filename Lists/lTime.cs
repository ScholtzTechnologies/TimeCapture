using System.Data;

namespace TimeCapture.Lists
{
    public class Time{
        public int TimeID { get; set; }
        public string Item { get; set; }
        public int TicketNo { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Total { get; set; }
        public string TimeType { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
    }
}
