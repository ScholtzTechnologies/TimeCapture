using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace TimeCapture.utils
{
    public static class _configuration
    {
        private static readonly IConfigurationRoot _config;
        private static readonly IConfigurationRoot _userConfig;
        private static string Config = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "config.json"));
        private static string UserConfig = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "userConfig.json"));
        
        public static string GetConfigFile
        {
            get
            {
                return Config;
            }
        }

        static _configuration()
        {
            if (_config == null)
            {
                _config = new ConfigurationBuilder()
                    .AddJsonFile(Config)
                    .Build();
            }

            if (_userConfig == null)
            {
                if (!File.Exists(UserConfig))
                {
                    using (var fileStream = File.Create(UserConfig)) { }
                    File.WriteAllText(UserConfig, JSON.GetBase());
                }

                _userConfig = new ConfigurationBuilder()
                    .AddJsonFile(UserConfig)
                    .Build();
            }
        }

        #region GetGlobalConfigs

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

        #endregion GetGlobalConfigs

        #region GetLocalConfigs

        public static bool isByPassValid(out int iUserID)
        {
            iUserID = 0;
            if (!_userConfig[ConfigKeys.Username].isNullOrEmpty() && !_userConfig[ConfigKeys.Password].isNullOrEmpty())
            {
                bool isValidLogin = new Access().Login(_userConfig[ConfigKeys.Username].Decrypt(), _userConfig[ConfigKeys.Password].Decrypt(), out iUserID);
                string sMachineName = Environment.MachineName;
                if (isValidLogin && _userConfig[ConfigKeys.MachineName].Decrypt() == sMachineName)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public static string GetLocalConfigValue(string Param)
        {
            try
            {
                return _userConfig[Param];
            }
            catch
            {
                return null;
            }
        }

        #endregion GetLocalConfigs

        #region AlterUpdateLocalConfig

        public static void UpdateLocalConfiguration(string key, string value, int iUserID)
        {
            var json = File.ReadAllText(UserConfig);
            var jObject = JObject.Parse(json);

            if (jObject.ContainsKey(key))
            {
                string sOldValue = GetLocalConfigValue(key) != null ? GetLocalConfigValue(key) : "";
                new Access().UpdateConfigHS(key, sOldValue, value, SQLMode.Modified, iUserID);
                jObject[key] = value;
            }
            else
            {
                new Access().UpdateConfigHS(key, "", value, SQLMode.Added, iUserID);
                jObject.Add(key, value);
            }

            File.WriteAllText(UserConfig, jObject.ToString());

            _userConfig.Reload();
        }

        public static void UpdateConfiguration(string key, string value, int iUserID)
        {
            var json = File.ReadAllText(Config);
            var jObject = JObject.Parse(json);

            if (jObject.ContainsKey(key))
            {
                string sOldValue = GetConfigValue(key) != null ? GetConfigValue(key) : "";
                new Access().UpdateConfigHS(key, sOldValue, value, SQLMode.Modified, -1);
                jObject[key] = value;
            }
            else
            {
                new Access().UpdateConfigHS(key, "", value, SQLMode.Added, -1);
                jObject.Add(key, value);
            }

            File.WriteAllText(Config, jObject.ToString());

            _config.Reload();
        }

        /// <summary>
        ///     This updates the configuration value used to skipped the login screen. data is Encrypted on set, and decrypted on get.
        /// </summary>
        public static void UpdateLocalUser(string sUsername, string sPassword, int iUserID)
        {
            UpdateLocalConfiguration(ConfigKeys.Username, sUsername.Encrypt(), iUserID);
            UpdateLocalConfiguration(ConfigKeys.MachineName, Environment.MachineName.Encrypt(), iUserID);
            UpdateLocalConfiguration(ConfigKeys.Password, sPassword.Encrypt(), iUserID);
        }

        #endregion AlterUpdateLocalConfig
    }
}
