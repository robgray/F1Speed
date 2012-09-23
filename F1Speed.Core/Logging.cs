using System;
using System.IO;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using log4net.Core;

namespace F1Speed.Core
{
    public class Logger
    {
        private readonly PatternLayout _layout = new PatternLayout();
        private const string LOG_PATTERN = "%d [%t] %-5p %m%n";

        public string DefaultPattern
        {
            get { return LOG_PATTERN; }
        }

        public Logger()
        {
            _layout.ConversionPattern = DefaultPattern;
            _layout.ActivateOptions();
        }

        public PatternLayout DefaultLayout
        {
            get { return _layout; }
        }

        public void AddAppender(IAppender appender)
        {
            var hierarchy =
                    (Hierarchy)LogManager.GetRepository();

            hierarchy.Root.AddAppender(appender);
        }

        static Logger()
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            var tracer = new TraceAppender();
            var patternLayout = new PatternLayout {ConversionPattern = LOG_PATTERN};

            patternLayout.ActivateOptions();

            tracer.Layout = patternLayout;
            tracer.ActivateOptions();
            hierarchy.Root.AddAppender(tracer);

            var roller = new RollingFileAppender
                             {
                                 Layout = patternLayout,
                                 AppendToFile = true,
                                 RollingStyle = RollingFileAppender.RollingMode.Size,
                                 MaxSizeRollBackups = 4,
                                 MaximumFileSize = "10240KB",
                                 StaticLogFileName = true,
                                 File = FilePath + @"\f1speed-log.txt"
                             };
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            hierarchy.Root.Level = Level.Debug;
            hierarchy.Configured = true;
        }

        private static string FilePath
        {
            get
            {
                var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var userFilePath = Path.Combine(localAppData, "F1Speed", "Logs");

                return userFilePath;
            }
        }

        public static ILog Create()
        {
            return LogManager.GetLogger("f1speed");
        }
    }

}
