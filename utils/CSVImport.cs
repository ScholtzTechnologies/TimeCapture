

namespace TimeCapture.utils
{
    public class CSVImport
    {
        public static string root = Directory.GetCurrentDirectory();

        public static string sFolder = Path.Combine(root, "csvImport");

        public static void CreateFolder()
        {
            if (!System.IO.Directory.Exists(sFolder))
            {
                Directory.CreateDirectory(sFolder);
            }
        }

        public class TimeSpanConverter : ITypeConverter
        {
            public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (TimeSpan.TryParseExact(text, @"h\:mm", CultureInfo.InvariantCulture, out TimeSpan result))
                {
                    return result;
                }
                throw new ArgumentException($"Invalid time format: {text}");
            }

            public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
            {
                return ((TimeSpan)value).ToString(@"hh\:mm");
            }
        }

        public class CSV
        {
            [Name("Item")]
            public string Item { get; set; }

            [Name("Ticket No")]
            public int TicketNo { get; set; }

            [Name("Start")]
            public string Start { get; set; }

            [Name("End")]
            public string End { get; set; }

            [Name("Total")]
            public string Total { get; set; }

            [Name("TimeType")]
            public string TimeType { get; set; }

            [Name("Description")]
            public string Description { get; set; }

            [Name("Type")]
            public string Type { get; set; }

            public string Date { get; set; }
        }

        public class Clients
        {
            public string Name { get; set; }
        }

        public class Types
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class Settings
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Value { get; set; }
            public string Desc { get; set; }
        }

        public class Tickets
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class Tasks
        {
            public int TaskID { get; set; }
            public string Task { get; set; }
            public int Status { get; set; }
        }
    }
}
