using System;
using Autofac.Extras.Moq;
using AutoFixture;
using Employees.Application.Accounts.Commands.ConfirmationEmail;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.UnitTests.Configuration
{
    public abstract class TestBase : IDisposable
    {
        protected readonly Fixture _fixture;
        protected readonly AutoMock _mock;

        protected TestBase()
        {
            _fixture = new Fixture();
            _mock = AutoMock.GetLoose();
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _mock?.Dispose();
            }
        }

        protected void LoggerMessageVerify<TCategoryName>(LogLevel level, string logMessage)
        {
            _mock.Mock<ILogger<TCategoryName>>().Verify(
                x => x.Log(
                    level,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals(logMessage, o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>) It.IsAny<object>()),
                Times.Once);
        }
    }
}