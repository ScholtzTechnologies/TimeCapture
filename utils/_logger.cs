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
        public void Log(LogType logType, string Text)
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

            if (logType == LogType.Info)
            {
                log.Info(Text);
            }
            else if (logType == LogType.Debug)
            {
                log.Debug(Text);
            }
            else if (logType == LogType.Error)
            {
                log.Error(Text);
            }
            else if (logType == LogType.Warn)
            {
                log.Warn(Text);
            }
            else if (logType == LogType.Fatal)
            {
                log.Fatal(Text);
            }
            else if (logType == LogType.Trace)
            {
                log.Trace(Text);
            }
        }
    }
}
