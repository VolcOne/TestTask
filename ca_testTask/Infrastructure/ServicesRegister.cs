using Autofac;
using ca_testTask.Handlers;
using ca_testTask.Model.Enums;
using ca_testTask.Services;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace ca_testTask.Infrastructure
{
    public sealed class ServicesRegister
    {
        public ServicesRegister(string pathLogFileResult = null) : this()
        {
            if (pathLogFileResult != null)
            {
                ((FileTarget)LogManager.Configuration.AllTargets[0]).FileName = pathLogFileResult;
            }
        }
        public ServicesRegister()
        {
            LogManager.Configuration = new XmlLoggingConfiguration("nlog.config", true);
            Resister();
        }
        public IContainer Container { get; private set; }

        private void Resister()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Runner>().As<IRunner>();
            builder.RegisterType<SurfaceScanner>().As<ISurfaceScanner>();
            builder.RegisterType<AllFileSearcher>().Keyed<IFileSearcher>(ActionType.All);
            builder.RegisterType<CppFileSearcher>().Keyed<IFileSearcher>(ActionType.Cpp);
            builder.RegisterType<Reversed1FileSearcher>().Keyed<IFileSearcher>(ActionType.Reversed1);
            builder.RegisterType<Reversed2FileSearcher>().Keyed<IFileSearcher>(ActionType.Reversed2);
            builder.RegisterInstance(LogManager.GetLogger("console")).As<ILogger>().SingleInstance();
            Container = builder.Build();
        }
    }
}