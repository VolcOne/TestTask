using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;

namespace ca_testTask.Services
{
    public class Runner : IRunner, IDisposable
    {
        private readonly ILogger _logger;

        public Runner(ILogger logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            _logger.Debug("Started");
        }

        public void Logging(IEnumerable<string> result)
        {
            foreach (var log in result)
            {
                _logger.Info(log);
            }
        }

        public void Dispose()
        {
            _logger.Debug("Finished");
        }
    }

    public interface IRunner
    {
        void Run();
        void Logging(IEnumerable<string> result);
    }
}