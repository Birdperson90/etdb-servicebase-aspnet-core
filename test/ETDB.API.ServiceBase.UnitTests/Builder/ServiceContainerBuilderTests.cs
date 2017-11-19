using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using ETDB.API.ServiceBase.Builder;
using ETDB.API.ServiceBase.Generics.Base;
using ETDB.API.ServiceBase.TestBase.Mocks;
using ETDB.API.ServiceBase.TestBase.TestClasses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ETDB.API.ServiceBase.UnitTests.Builder
{
    [TestClass]
    public class ServiceContainerBuilderTests
    {
        private ServiceContainerBuilder serviceContainerBuilder;
        private ServiceCollection serviceCollection;
        private ContainerBuilder containerBuilder;

        public ServiceContainerBuilderTests()
        {
            this.serviceCollection = new ServiceCollection();
            this.containerBuilder = new ContainerBuilder();
            this.serviceContainerBuilder = new ServiceContainerBuilder(containerBuilder);
        }

        [TestMethod]
        public void ServiceContainerBuilder_RegisterTypePerDependencyResolveTwice_ExpectDifferentInstances()
        {
            this.serviceContainerBuilder.RegisterTypePerDependency<TestImplementation, ITestInterface>();
            var container = this.serviceContainerBuilder.Build(serviceCollection);
            var firstInstance = container.Resolve<ITestInterface>();

            Assert.IsNotNull(firstInstance);
            Assert.IsTrue(typeof(ITestInterface).IsAssignableFrom(typeof(TestImplementation)));

            var secondInstance = container.Resolve<ITestInterface>();
            Assert.IsNotNull(secondInstance);
            Assert.AreNotEqual(firstInstance, secondInstance);
        }

        [TestMethod]
        public void ServiceContainerBuilder_RegisterGenericRepositoryResolveTwice_ExpectSameInstance()
        {
            this.serviceCollection.AddDbContext<DbContextMock>();
            this.serviceContainerBuilder.UseGenericRepositoryPattern<DbContextMock>();
            var container = this.serviceContainerBuilder.Build(this.serviceCollection);
            var firstInstance = container.Resolve<IEntityRepository<EntityMock>>();
            Assert.IsNotNull(firstInstance);
            var secondInstance = container.Resolve<IEntityRepository<EntityMock>>();
            Assert.AreEqual(firstInstance, secondInstance);
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.serviceContainerBuilder = null;
            this.containerBuilder = null;
            this.serviceCollection = null;
        }
    }
}
