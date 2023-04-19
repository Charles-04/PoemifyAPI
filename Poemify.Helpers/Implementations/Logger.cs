﻿using NLog;
using Poemify.Helpers.Interfaces;


namespace Poemify.Helpers.Implementations
{
    public class Logger : ILogManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public Logger()
        {

        }

        public void LogDebug(string message) => logger.Debug(message);
        

        public void LogError(string message) => logger.Error(message);
       

        public void LogInfo(string message) => logger.Info(message);
       

        public void LogWarn(string message) => logger.Warn(message);   
       
    }
}
