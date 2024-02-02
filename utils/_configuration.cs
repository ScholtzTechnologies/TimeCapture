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
        private static string Config = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "config.json"));
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

        public static int isBusiness
        {
            get {
                string sBool = _config["isBusiness"];
                if (sBool.ToLower() == "y")
                    return 1;
                else if (sBool.ToLower() == "n")
                    return 0;
                else if (sBool.ToLower() == "1")
                    return 1;
                else if (sBool.ToLower() == "0")
                    return 0;
                else
                    return 0;
            }
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
                    return _config["SQL"];
                }
                return "";
            }
        }
    }
}
