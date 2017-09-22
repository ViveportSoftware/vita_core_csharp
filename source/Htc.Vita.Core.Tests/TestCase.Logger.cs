using System;
using Htc.Vita.Core.Log;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public partial class TestCase
    {
        [Fact]
        public void Logger_Default_0_GetInstance()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
        }

        [Fact]
        public void Logger_Default_0_GetInstance_WithName()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance("Alt");
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Default_0_GetInstance_WithType()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Default_1_Debug()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Debug("Default test debug message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void Logger_Default_1_Debug_WithName()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance("Alt");
            Assert.NotNull(loggerAlt);
            logger.Debug("Default test debug message");
            loggerAlt.Debug("Default test debug message in Alt", new Exception("Alt"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Default_1_Debug_WithType()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Debug("Default test debug message");
            loggerAlt.Debug("Default test debug message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Default_2_Error()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Error("Default test error message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void Logger_Default_2_Error_WithName()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance("Alt");
            Assert.NotNull(loggerAlt);
            logger.Error("Default test error message");
            loggerAlt.Error("Default test error message in Alt", new Exception("Alt"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Default_2_Error_WithType()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Error("Default test error message");
            loggerAlt.Error("Default test error message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Default_3_Fatal()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Fatal("Default test fatal message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void Logger_Default_3_Fatal_WithName()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance("Alt");
            Assert.NotNull(loggerAlt);
            logger.Fatal("Default test fatal message");
            loggerAlt.Fatal("Default test fatal message in Alt", new Exception("Alt"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Default_3_Fatal_WithType()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Fatal("Default test fatal message");
            loggerAlt.Fatal("Default test fatal message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Default_4_Info()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Info("Default test info message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void Logger_Default_4_Info_WithName()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance("Alt");
            Assert.NotNull(loggerAlt);
            logger.Info("Default test info message");
            loggerAlt.Info("Default test info message in Alt", new Exception("Alt"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Default_4_Info_WithType()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Info("Default test info message");
            loggerAlt.Info("Default test info message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Default_5_Trace()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Trace("Default test trace message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void Logger_Default_5_Trace_WithName()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance("Alt");
            Assert.NotNull(loggerAlt);
            logger.Trace("Default test trace message");
            loggerAlt.Trace("Default test trace message in Alt", new Exception("Alt"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Default_5_Trace_WithType()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Trace("Default test trace message");
            loggerAlt.Trace("Default test trace message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Default_6_Warn()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Warn("Default test warn message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void Logger_Default_6_Warn_WithName()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance("Alt");
            Assert.NotNull(loggerAlt);
            logger.Warn("Default test warn message");
            loggerAlt.Warn("Default test warn message in Alt", new Exception("Alt"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Default_6_Warn_WithType()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Warn("Default test warn message");
            loggerAlt.Warn("Default test warn message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Default_7_Shutdown()
        {
            var logger = Logger.GetInstance();
            Assert.NotNull(logger);
            logger.Shutdown();
        }

        [Fact]
        public void Logger_Console_0_GetInstance()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
        }

        [Fact]
        public void Logger_Console_0_GetInstance_WithName()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<ConsoleLogger>("Alt");
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Console_0_GetInstance_WithType()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<ConsoleLogger>(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Console_1_Debug()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            logger.Debug("Console test debug message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void Logger_Console_1_Debug_WithName()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<ConsoleLogger>("Alt");
            Assert.NotNull(loggerAlt);
            logger.Debug("Console test debug message");
            loggerAlt.Debug("Console test debug message in Alt", new Exception("Alt"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Console_1_Debug_WithType()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<ConsoleLogger>(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Debug("Console test debug message");
            loggerAlt.Debug("Console test debug message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Console_2_Error()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            logger.Error("Console test error message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void Logger_Console_2_Error_WithName()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<ConsoleLogger>("Alt");
            Assert.NotNull(loggerAlt);
            logger.Error("Console test error message");
            loggerAlt.Error("Console test error message in Alt", new Exception("Alt"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Console_2_Error_WithType()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<ConsoleLogger>(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Error("Console test error message");
            loggerAlt.Error("Console test error message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Console_3_Fatal()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            logger.Fatal("Console test fatal message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void Logger_Console_3_Fatal_WithName()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<ConsoleLogger>("Alt");
            Assert.NotNull(loggerAlt);
            logger.Fatal("Console test fatal message");
            loggerAlt.Fatal("Console test fatal message in Alt", new Exception("Alt"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Console_3_Fatal_WithType()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<ConsoleLogger>(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Fatal("Console test fatal message");
            loggerAlt.Fatal("Console test fatal message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Console_4_Info()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            logger.Info("Console test info message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void Logger_Console_4_Info_WithName()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<ConsoleLogger>("Alt");
            Assert.NotNull(loggerAlt);
            logger.Info("Console test info message");
            loggerAlt.Info("Console test info message in Alt", new Exception("Alt"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Console_4_Info_WithType()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<ConsoleLogger>(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Info("Console test info message");
            loggerAlt.Info("Console test info message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Console_5_Trace()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            logger.Trace("Console test trace message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void Logger_Console_5_Trace_WithName()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<ConsoleLogger>("Alt");
            Assert.NotNull(loggerAlt);
            logger.Trace("Console test trace message");
            loggerAlt.Trace("Console test trace message in Alt", new Exception("Alt"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Console_5_Trace_WithType()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<ConsoleLogger>(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Trace("Console test trace message");
            loggerAlt.Trace("Console test trace message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Console_6_Warn()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            logger.Warn("Console test warn message");
            Assert.NotNull(logger);
        }

        [Fact]
        public void Logger_Console_6_Warn_WithName()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<ConsoleLogger>("Alt");
            Assert.NotNull(loggerAlt);
            logger.Warn("Console test warn message");
            loggerAlt.Warn("Console test warn message in Alt", new Exception("Alt"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Console_6_Warn_WithType()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<ConsoleLogger>(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Warn("Console test warn message");
            loggerAlt.Warn("Console test warn message in TestCase", new Exception("TestCase"));
            Assert.NotNull(logger);
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Console_7_Shutdown()
        {
            var logger = Logger.GetInstance<ConsoleLogger>();
            Assert.NotNull(logger);
            logger.Shutdown();
        }

        [Fact]
        public void Logger_Dummy_0_GetInstance()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
        }

        [Fact]
        public void Logger_Dummy_0_GetInstance_WithName()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<DummyLogger>("Alt");
            Assert.NotNull(loggerAlt);
            Assert.NotSame(logger, loggerAlt);
        }

        [Fact]
        public void Logger_Dummy_1_Debug()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            logger.Debug("Dummy test debug message");
            Assert.True(logger is DummyLogger);
            var dummyLogger = (DummyLogger)logger;
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test debug message"));
        }

        [Fact]
        public void Logger_Dummy_1_Debug_WithName()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<DummyLogger>("Alt");
            Assert.NotNull(loggerAlt);
            logger.Debug("Dummy test debug message");
            loggerAlt.Debug("Dummy test debug message in Alt", new Exception("Alt"));
            var dummyLogger = (DummyLogger)logger;
            var dummyLoggerAlt = (DummyLogger)loggerAlt;
            Assert.NotNull(dummyLogger);
            Assert.NotNull(dummyLoggerAlt);
            Assert.NotSame(dummyLogger, dummyLoggerAlt);
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test debug message"));
            Assert.True(dummyLoggerAlt.GetBuffer().EndsWith("Dummy test debug message in Alt" + new Exception("Alt")));
        }

        [Fact]
        public void Logger_Dummy_1_Debug_WithType()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<DummyLogger>(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Debug("Dummy test debug message");
            loggerAlt.Debug("Dummy test debug message in TestCase", new Exception("TestCase"));
            var dummyLogger = (DummyLogger)logger;
            var dummyLoggerAlt = (DummyLogger)loggerAlt;
            Assert.NotNull(dummyLogger);
            Assert.NotNull(dummyLoggerAlt);
            Assert.NotSame(dummyLogger, dummyLoggerAlt);
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test debug message"));
            Assert.True(dummyLoggerAlt.GetBuffer().EndsWith("Dummy test debug message in TestCase" + new Exception("TestCase")));
        }

        [Fact]
        public void Logger_Dummy_2_Error()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            logger.Error("Dummy test error message");
            Assert.True(logger is DummyLogger);
            var dummyLogger = (DummyLogger)logger;
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test error message"));
        }

        [Fact]
        public void Logger_Dummy_2_Error_WithName()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<DummyLogger>("Alt");
            Assert.NotNull(loggerAlt);
            logger.Error("Dummy test error message");
            loggerAlt.Error("Dummy test error message in Alt", new Exception("Alt"));
            var dummyLogger = (DummyLogger)logger;
            var dummyLoggerAlt = (DummyLogger)loggerAlt;
            Assert.NotNull(dummyLogger);
            Assert.NotNull(dummyLoggerAlt);
            Assert.NotSame(dummyLogger, dummyLoggerAlt);
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test error message"));
            Assert.True(dummyLoggerAlt.GetBuffer().EndsWith("Dummy test error message in Alt" + new Exception("Alt")));
        }

        [Fact]
        public void Logger_Dummy_2_Error_WithType()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<DummyLogger>(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Error("Dummy test error message");
            loggerAlt.Error("Dummy test error message in TestCase", new Exception("TestCase"));
            var dummyLogger = (DummyLogger)logger;
            var dummyLoggerAlt = (DummyLogger)loggerAlt;
            Assert.NotNull(dummyLogger);
            Assert.NotNull(dummyLoggerAlt);
            Assert.NotSame(dummyLogger, dummyLoggerAlt);
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test error message"));
            Assert.True(dummyLoggerAlt.GetBuffer().EndsWith("Dummy test error message in TestCase" + new Exception("TestCase")));
        }

        [Fact]
        public void Logger_Dummy_3_Fatal()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            logger.Fatal("Dummy test fatal message");
            Assert.True(logger is DummyLogger);
            var dummyLogger = (DummyLogger)logger;
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test fatal message"));
        }

        [Fact]
        public void Logger_Dummy_3_Fatal_WithName()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<DummyLogger>("Alt");
            Assert.NotNull(loggerAlt);
            logger.Fatal("Dummy test fatal message");
            loggerAlt.Fatal("Dummy test fatal message in Alt", new Exception("Alt"));
            var dummyLogger = (DummyLogger)logger;
            var dummyLoggerAlt = (DummyLogger)loggerAlt;
            Assert.NotNull(dummyLogger);
            Assert.NotNull(dummyLoggerAlt);
            Assert.NotSame(dummyLogger, dummyLoggerAlt);
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test fatal message"));
            Assert.True(dummyLoggerAlt.GetBuffer().EndsWith("Dummy test fatal message in Alt" + new Exception("Alt")));
        }

        [Fact]
        public void Logger_Dummy_3_Fatal_WithType()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<DummyLogger>(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Fatal("Dummy test fatal message");
            loggerAlt.Fatal("Dummy test fatal message in TestCase", new Exception("TestCase"));
            var dummyLogger = (DummyLogger)logger;
            var dummyLoggerAlt = (DummyLogger)loggerAlt;
            Assert.NotNull(dummyLogger);
            Assert.NotNull(dummyLoggerAlt);
            Assert.NotSame(dummyLogger, dummyLoggerAlt);
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test fatal message"));
            Assert.True(dummyLoggerAlt.GetBuffer().EndsWith("Dummy test fatal message in TestCase" + new Exception("TestCase")));
        }

        [Fact]
        public void Logger_Dummy_4_Info()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            logger.Info("Dummy test info message");
            Assert.True(logger is DummyLogger);
            var dummyLogger = (DummyLogger)logger;
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test info message"));
        }

        [Fact]
        public void Logger_Dummy_4_Info_WithName()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<DummyLogger>("Alt");
            Assert.NotNull(loggerAlt);
            logger.Info("Dummy test info message");
            loggerAlt.Info("Dummy test info message in Alt", new Exception("Alt"));
            var dummyLogger = (DummyLogger)logger;
            var dummyLoggerAlt = (DummyLogger)loggerAlt;
            Assert.NotNull(dummyLogger);
            Assert.NotNull(dummyLoggerAlt);
            Assert.NotSame(dummyLogger, dummyLoggerAlt);
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test info message"));
            Assert.True(dummyLoggerAlt.GetBuffer().EndsWith("Dummy test info message in Alt" + new Exception("Alt")));
        }

        [Fact]
        public void Logger_Dummy_4_Info_WithType()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<DummyLogger>(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Info("Dummy test info message");
            loggerAlt.Info("Dummy test info message in TestCase", new Exception("TestCase"));
            var dummyLogger = (DummyLogger)logger;
            var dummyLoggerAlt = (DummyLogger)loggerAlt;
            Assert.NotNull(dummyLogger);
            Assert.NotNull(dummyLoggerAlt);
            Assert.NotSame(dummyLogger, dummyLoggerAlt);
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test info message"));
            Assert.True(dummyLoggerAlt.GetBuffer().EndsWith("Dummy test info message in TestCase" + new Exception("TestCase")));
        }

        [Fact]
        public void Logger_Dummy_5_Trace()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            logger.Trace("Dummy test trace message");
            Assert.True(logger is DummyLogger);
            var dummyLogger = (DummyLogger)logger;
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test trace message"));
        }

        [Fact]
        public void Logger_Dummy_5_Trace_WithName()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<DummyLogger>("Alt");
            Assert.NotNull(loggerAlt);
            logger.Trace("Dummy test trace message");
            loggerAlt.Trace("Dummy test trace message in Alt", new Exception("Alt"));
            var dummyLogger = (DummyLogger)logger;
            var dummyLoggerAlt = (DummyLogger)loggerAlt;
            Assert.NotNull(dummyLogger);
            Assert.NotNull(dummyLoggerAlt);
            Assert.NotSame(dummyLogger, dummyLoggerAlt);
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test trace message"));
            Assert.True(dummyLoggerAlt.GetBuffer().EndsWith("Dummy test trace message in Alt" + new Exception("Alt")));
        }

        [Fact]
        public void Logger_Dummy_5_Trace_WithType()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<DummyLogger>(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Trace("Dummy test trace message");
            loggerAlt.Trace("Dummy test trace message in TestCase", new Exception("TestCase"));
            var dummyLogger = (DummyLogger)logger;
            var dummyLoggerAlt = (DummyLogger)loggerAlt;
            Assert.NotNull(dummyLogger);
            Assert.NotNull(dummyLoggerAlt);
            Assert.NotSame(dummyLogger, dummyLoggerAlt);
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test trace message"));
            Assert.True(dummyLoggerAlt.GetBuffer().EndsWith("Dummy test trace message in TestCase" + new Exception("TestCase")));
        }

        [Fact]
        public void Logger_Dummy_6_Warn()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            logger.Warn("Dummy test warn message");
            Assert.True(logger is DummyLogger);
            var dummyLogger = (DummyLogger)logger;
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test warn message"));
        }

        [Fact]
        public void Logger_Dummy_6_Warn_WithName()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<DummyLogger>("Alt");
            Assert.NotNull(loggerAlt);
            logger.Warn("Dummy test warn message");
            loggerAlt.Warn("Dummy test warn message in Alt", new Exception("Alt"));
            var dummyLogger = (DummyLogger)logger;
            var dummyLoggerAlt = (DummyLogger)loggerAlt;
            Assert.NotNull(dummyLogger);
            Assert.NotNull(dummyLoggerAlt);
            Assert.NotSame(dummyLogger, dummyLoggerAlt);
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test warn message"));
            Assert.True(dummyLoggerAlt.GetBuffer().EndsWith("Dummy test warn message in Alt" + new Exception("Alt")));
        }

        [Fact]
        public void Logger_Dummy_6_Warn_WithType()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            var loggerAlt = Logger.GetInstance<DummyLogger>(typeof(TestCase));
            Assert.NotNull(loggerAlt);
            logger.Warn("Dummy test warn message");
            loggerAlt.Warn("Dummy test warn message in TestCase", new Exception("TestCase"));
            var dummyLogger = (DummyLogger)logger;
            var dummyLoggerAlt = (DummyLogger)loggerAlt;
            Assert.NotNull(dummyLogger);
            Assert.NotNull(dummyLoggerAlt);
            Assert.NotSame(dummyLogger, dummyLoggerAlt);
            Assert.True(dummyLogger.GetBuffer().EndsWith("Dummy test warn message"));
            Assert.True(dummyLoggerAlt.GetBuffer().EndsWith("Dummy test warn message in TestCase" + new Exception("TestCase")));
        }

        [Fact]
        public void Logger_Dummy_7_Shutdown()
        {
            var logger = Logger.GetInstance<DummyLogger>();
            Assert.NotNull(logger);
            logger.Shutdown();
        }
    }
}
