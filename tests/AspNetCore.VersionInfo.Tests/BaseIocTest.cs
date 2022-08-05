using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AspNetCore.VersionInfo.Tests
{
    public class BaseIocTest
    {
        protected Mock<IServiceProvider> _serviceProvider;
        protected Mock<IServiceScopeFactory> _serviceScopeFactory;
        protected Mock<IServiceScope> _serviceScope;

        public BaseIocTest()
        {
            InitIoc();
        }

        protected void InitIoc()
        {
            _serviceProvider = new Mock<IServiceProvider>();

            _serviceScope = new Mock<IServiceScope>();
            _serviceScope.Setup(x => x.ServiceProvider).Returns(_serviceProvider.Object);

            _serviceScopeFactory = new Mock<IServiceScopeFactory>();
            _serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(_serviceScope.Object);

            _serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(_serviceScopeFactory.Object);
        }

        protected void RegisterServiceWithInstance<T>(T obj)
        {
            _serviceProvider
                .Setup(x => x.GetService(typeof(T)))
                .Returns(obj);
        }
    }
}