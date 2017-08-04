using System;
using System.Threading.Tasks;
using Autofac;
using ca_testTask.Handlers;
using ca_testTask.Infrastructure;
using ca_testTask.Model.Enums;
using ca_testTask.Services;

namespace ca_testTask
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "?")
            {
                Console.WriteLine(GetHelp());
                return;
            }
            var path = args[0];
            var action = (ActionType)Enum.Parse(typeof(ActionType), args.Length > 1 ? args[1] : "all", true);
            var pathLogFileResult = args.Length == 3 ? args[2] : null;

            Task.Run(() => StartAsync(path, action, pathLogFileResult)).GetAwaiter().GetResult();

            Console.WriteLine("\nProcess finished. For exit press any key");
            Console.ReadKey();
        }

        private static async Task StartAsync(string path, ActionType action, string pathLogFileResult)
        {
            try
            {
                ServicesRegister sReg = new ServicesRegister(pathLogFileResult);
                using (var scope = sReg.Container.BeginLifetimeScope())
                {
                    var runner = scope.Resolve<IRunner>();
                    runner.Run();
                    var fileSearcher = scope.ResolveKeyed<IFileSearcher>(action);
                    var result = await fileSearcher.GetFilePaths(path);
                    runner.Logging(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                //throw;
            }
        }

        private static string GetHelp()
        {
            return @"Usage: testTask <startPath> [<action> <logFile>]

<startPath> - will start search files from that folder
<action> - [all|cpp|reversed1|reversed2] by default ""all""
<logFile> - name of file for logging by default ""results.txt""";
        }
    }
}
