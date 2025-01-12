using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace Usuario
{
    public class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IConfiguration _configuration;

        public Logger(IConfiguration configuration)
        {
            _configuration = configuration;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("Entry assembly is null"));
            var log4netConfigFile = _configuration["Log4NetCore:configFile"];
            XmlConfigurator.Configure(logRepository, new FileInfo(log4netConfigFile));
        }

        public void LogInfo(string message)
        {
            log.Info(message);
        }

        public void LogError(string message, Exception ex)
        {
            log.Error(message, ex);
        }

        public void LogDebug(string message)
        {
            log.Debug(message);
        }

        public void LogWarn(string message)
        {
            log.Warn(message);
        }
    }
}
