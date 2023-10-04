using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TimeCapture.utils
{
    public static class _configuration
    {
        private static readonly IConfigurationRoot _config;
        private static string Config = Path.GetFullPath("../../../config.json");
        static _configuration()
        {
            if (_config == null)
            {
                _config = new ConfigurationBuilder()
                    .AddJsonFile(Config)
                    .Build();
            }
        }

        public static string ConnectionString
        {
            get { return _config["ConnectionString"]; }
        }

        public static string isBusiness
        {
            get { return _config["isBusiness"]; }
        }

        public static string BusinessName
        {
            get { return _config["BusinessName"]; }
        }

        public static string isNotifications
        {
            get { return _config["isNotifications"]; }
        }

        public static string SeleniumURL
        {
            get { return _config["SeleniumURL"]; }
        }

        public static string DateTimeFormat
        {
            get { return _config["DateTimeFormat"]; }
        }

        public static string GetConfigValue(string Param)
        {
            try
            {
                return _config[Param];
            }
            catch
            {
                return null;
            }
        }

        public static string GetDirectUpdate
        {
            get
            {
                StringBuilder SQL = new StringBuilder();
                if (_config["IsDirectCapture"] == "1")
                {
                    SQL.AppendLine(_config["SQL01"]);
                    SQL.AppendLine(_config["SQL02"]);
                    SQL.AppendLine(_config["SQL03"]);
                    return SQL.ToString();
                }
                return "";
            }
        }
    }
}
