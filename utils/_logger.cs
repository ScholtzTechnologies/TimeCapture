using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace TimeCapture.utils
{
    public class _logger
    {
        public void Log(LogType logType, string Text, string sSection = null)
        {
            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget
            {
                FileName = "Logs",
                Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}",
            };

            config.AddTarget("file", fileTarget);

            LogManager.Configuration = config;
            Logger log = LogManager.GetCurrentClassLogger();

            if (sSection != null)
            {
                Text = $"[{sSection}] : {Text}";
            }

            switch (logType)
            {
                case LogType.Info:
                    log.Info(Text);
                    break;

                case LogType.Debug:
                    log.Debug(Text);
                    break;

                case LogType.Error:
                    log.Error(Text);
                    break;


                case LogType.Warn:
                    log.Warn(Text);
                    break;

                case LogType.Fatal:
                    log.Fatal(Text);
                    break;

                case LogType.Trace:
                    log.Trace(Text);
                    break;

                default:
                    log.Info(Text);
                    break;
            }
        }
    }
}
