using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace AspNetCore.VersionInfo.Tests
{
    // Based on https://adamstorr.azurewebsites.net/blog/mocking-ilogger-with-moq
    // https://github.com/WestDiscGolf/Random/tree/master/LoggerUnitTests
    public static class LoggerTestExtensions
    {
        public static Mock<ILogger<T>> VerifyDebugWasCalled<T>(this Mock<ILogger<T>> logger, string expectedMessage)
        {
            Func<object, Type, bool> state = (v, t) => v.ToString().CompareTo(expectedMessage) == 0;

            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Debug),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => state(v, t)),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));

            return logger;
        }

        public static Mock<ILogger<T>> VerifyDebugWasCalled<T>(this Mock<ILogger<T>> logger)
        {
            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Debug),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));

            return logger;
        }

        public static Mock<ILogger<T>> VerifyLogging<T>(this Mock<ILogger<T>> logger, string expectedMessage, LogLevel expectedLogLevel = LogLevel.Debug, Times? times = null)
        {
            times ??= Times.Once();

            Func<object, Type, bool> stateMessage = (v, t) => v.ToString().CompareTo(expectedMessage) == 0;

            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == expectedLogLevel),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => stateMessage(v, t)),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), (Times)times);

            return logger;
        }

        public static Mock<ILogger<T>> VerifyLogging<T>(this Mock<ILogger<T>> logger, string expectedMessage, Exception exception, LogLevel expectedLogLevel = LogLevel.Debug, Times? times = null)
        {
            times ??= Times.Once();

            Func<object, Type, bool> stateMessage = (v, t) => v.ToString().Equals(expectedMessage, StringComparison.CurrentCultureIgnoreCase);
            Func<object, Type, bool> stateException = (v, t) => v.GetType() == exception.GetType()
            && (v as Exception)?.Message.Equals(exception.Message, StringComparison.CurrentCultureIgnoreCase) == true;


            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == expectedLogLevel),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => stateMessage(v, t)),
                    It.Is<Exception>((v, t) => stateException(v, t)),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), (Times)times);

            return logger;
        }
    }
}
