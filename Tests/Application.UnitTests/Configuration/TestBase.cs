using System;
using Autofac.Extras.Moq;
using AutoFixture;

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
    }
}