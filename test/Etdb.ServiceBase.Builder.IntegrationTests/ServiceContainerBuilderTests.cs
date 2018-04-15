using System;
using Autofac;
using Etdb.ServiceBase.Builder.Builder;
using Etdb.ServiceBase.DocumentRepository.Abstractions.Context;
using Etdb.ServiceBase.DocumentRepository.Abstractions.Generics;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Context;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Documents;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Xunit;

namespace Etdb.ServiceBase.Builder.IntegrationTests
{
    public class ServiceContainerBuilderTests : IDisposable
    {
        private readonly ServiceContainerBuilder containerBuilder;
        private readonly IServiceCollection services;
        private IContainer container;
        
        public ServiceContainerBuilderTests()
        {
            this.containerBuilder = new ServiceContainerBuilder(new ContainerBuilder());
            this.services = new ServiceCollection();
        }
        
        [Fact]
        public void ServiceContainerBuilder_UseConfigurationAndResolve_ExpectInstance()
        {
            var configuration = new ConfigurationBuilder()
                .Build();

            this.containerBuilder.UseConfiguration(configuration);
            this.BuildContainer();

            var resolvedConfigurationRoot = this.container.Resolve<IConfigurationRoot>();
            
            Assert.Equal(configuration, resolvedConfigurationRoot);

            var resolvedConfiguration = this.container.Resolve<IConfiguration>();
            
            Assert.Equal(configuration, resolvedConfiguration);
        }

        [Fact]
        public void ServiceContainerBuilder_UseHostingEnvironmentAndResolve_ExpectInstance()
        {
            var environment = new HostingEnvironment();

            this.containerBuilder.UseEnvironment(environment);
            this.BuildContainer();

            var resolvedEnv = this.container.Resolve<IHostingEnvironment>();
            
            Assert.Equal(environment, resolvedEnv);
        }
        
        [Fact]
        public void ServiceContainerBuilder_UseGenericDocumentRepositoryPatternValidInput_ExpectInstance()
        {
            services.AddOptions();
            this.services.Configure<DocumentDbContextOptions>(options =>
            {
                options.ConnectionString = "mongodb://admin:admin@localhost:27017";
                options.DatabaseName = "Etdb_ServiceBase_Tests";
            });
            
            this.containerBuilder.UseGenericDocumentRepositoryPattern<TestDocumentDbContext>(typeof(TestDocumentDbContext).Assembly);
            
            this.BuildContainer();
            
            Assert.True(this.container.IsRegistered<IOptions<DocumentDbContextOptions>>(), "DbContextOptions not registered!");
            Assert.True(this.container.IsRegistered<TestDocumentDbContext>(), "DbContext not registered!");

            var genericRepository = this.container.Resolve<IDocumentRepository<TodoListDocument, Guid>>();
            var implementedInterfaceRepository = this.container.Resolve<ITodoListDocumentRepository>();
            
            Assert.Equal(genericRepository, implementedInterfaceRepository);
        }

        private void BuildContainer()
        {
            this.container = this.containerBuilder.Build(this.services);
        }

        public void Dispose()
        {
            this.container?.Dispose();
        }
    }
}