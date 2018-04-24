using System;
using System.Collections.Generic;
using Autofac;
using AutoMapper;
using Etdb.ServiceBase.Builder.Builder;
using Etdb.ServiceBase.Cqrs.Abstractions.Bus;
using Etdb.ServiceBase.Cqrs.Abstractions.Handler;
using Etdb.ServiceBase.Cqrs.Abstractions.Validation;
using Etdb.ServiceBase.DocumentRepository.Abstractions.Context;
using Etdb.ServiceBase.DocumentRepository.Abstractions.Generics;
using Etdb.ServiceBase.EntityRepository.Abstractions.Generics;
using Etdb.ServiceBase.TestInfrastructure.AutoMapper.DataTransferObjects;
using Etdb.ServiceBase.TestInfrastructure.AutoMapper.Profiles;
using Etdb.ServiceBase.TestInfrastructure.AutoMapper.Resolver;
using Etdb.ServiceBase.TestInfrastructure.Cqrs.Commands;
using Etdb.ServiceBase.TestInfrastructure.EntityFramework.Context;
using Etdb.ServiceBase.TestInfrastructure.EntityFramework.Entities;
using Etdb.ServiceBase.TestInfrastructure.EntityFramework.Repositories;
using Etdb.ServiceBase.TestInfrastructure.Misc;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Context;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Documents;
using Etdb.ServiceBase.TestInfrastructure.MongoDb.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
        public void ServiceContainerBuilder_UseConfigurationAndResolve_ExpectInstances()
        {
            var configuration = new ConfigurationBuilder()
                .Build();

            this.containerBuilder.UseConfiguration(configuration);
            this.BuildContainer();
            
            Assert.True(this.container.IsRegistered<IConfigurationRoot>(), "IConfigurationRoot not registered!");
            Assert.True(this.container.IsRegistered<IConfiguration>(), "IConfiguration not registered!");

            var resolvedConfigurationRoot = this.container.Resolve<IConfigurationRoot>();
            
            Assert.Equal(configuration, resolvedConfigurationRoot);

            var resolvedConfiguration = this.container.Resolve<IConfiguration>();
            
            Assert.Equal(configuration, resolvedConfiguration);
        }

        [Fact]
        public void ServiceContainerBuilder_UseHostingEnvironmentAndResolve_ExpectInstances()
        {
            var environment = new HostingEnvironment();

            this.containerBuilder.UseEnvironment(environment);
            
            this.BuildContainer();

            Assert.True(this.container.IsRegistered<IHostingEnvironment>(), "IHostingEnvironment not registered!");
            
            var resolvedEnv = this.container.Resolve<IHostingEnvironment>();
            
            Assert.Equal(environment, resolvedEnv);
        }
        
        [Fact]
        public void ServiceContainerBuilder_UseGenericDocumentRepositoryPatternValidInput_ExpectInstances()
        {
            this.services.AddOptions();
            
            this.services.Configure<DocumentDbContextOptions>(options =>
            {
                options.ConnectionString = "mongodb://admin:admin@localhost:27017";
                options.DatabaseName = "Etdb_ServiceBase_Tests";
            });
            
            this.containerBuilder.UseGenericDocumentRepositoryPattern<TestDocumentDbContext>(typeof(TestDocumentDbContext).Assembly);
            
            this.BuildContainer();
            
            Assert.True(this.container.IsRegistered<IOptions<DocumentDbContextOptions>>(), "DbContextOptions not registered!");
            Assert.True(this.container.IsRegistered<TestDocumentDbContext>(), "DbContext not registered!");
            Assert.True(this.container.IsRegistered<IDocumentRepository<TodoListDocument, Guid>>(), "Base repository not registered!");
            Assert.True(this.container.IsRegistered<ITodoListDocumentRepository>(), "Implemented repository interface not registered!");

            var genericRepository = this.container.Resolve<IDocumentRepository<TodoListDocument, Guid>>();
            var implementedInterfaceRepository = this.container.Resolve<ITodoListDocumentRepository>();

            Assert.Equal(genericRepository, implementedInterfaceRepository);
        }
        
        [Fact]
        public void ServiceContainerBuilder_UseGenericDocumentRepositoryPatternInvalidInput_ExpectException()
        {
            Assert.Throws<ArgumentException>(() => this.containerBuilder
                .UseGenericDocumentRepositoryPattern<TestDocumentDbContext>());
        }
        
        [Fact]
        public void ServiceContainerBuilder_UseGenericEntityRepositoryPatternValidInput_ExpectInstances()
        {
            this.containerBuilder.UseGenericEntityRepositoryPattern<InMemoryEntityDbContext>(typeof(InMemoryEntityDbContext).Assembly);
            
            this.BuildContainer();
            
            Assert.True(this.container.IsRegistered<InMemoryEntityDbContext>(), "DbContext not registered!");
            Assert.True(this.container.IsRegistered<IEntityRepository<TodoListEntity, Guid>>(), "Base repository not registered!");
            Assert.True(this.container.IsRegistered<ITodoListEntityRepository>(), "Implemented repository interface not registered!");

            var genericRepository = this.container.Resolve<IEntityRepository<TodoListEntity, Guid>>();
            var implementedInterfaceRepository = this.container.Resolve<ITodoListEntityRepository>();

            Assert.Equal(genericRepository, implementedInterfaceRepository);
        }
        
        [Fact]
        public void ServiceContainerBuilder_UseGenericEntityRepositoryPatternInvalidInput_ExpectException()
        {
            Assert.Throws<ArgumentException>(() =>
                this.containerBuilder.UseGenericEntityRepositoryPattern<InMemoryEntityDbContext>());
        }

        [Fact]
        public void ServiceContainerBuilder_UseAutoMapperValidInput_ExpectInstances()
        {
            this.containerBuilder.UseAutoMapper(typeof(TodoProfile).Assembly);
            this.BuildContainer();
            
            Assert.True(this.container.IsRegistered<IMapper>());
            Assert.True(this.container.IsRegistered<MapperConfiguration>());
            Assert.True(this.container.IsRegistered<TodoDtoIdResolver>());

            var profiles = this.container.Resolve<IEnumerable<Profile>>();

            Assert.Single(profiles);
        }

        [Fact]
        public void ServiceContainerBuilder_UseCqrsValidInput_ExpectInstances()
        {
            this.containerBuilder.UseCqrs(typeof(SimpleVoidCommand).Assembly);
            this.BuildContainer();
            
            Assert.True(this.container.IsRegistered<IMediator>());
            Assert.True(this.container.IsRegistered<IMediatorHandler>());
            
            Assert.True(this.container.IsRegistered<IVoidCommandValidation<ComplexVoidCommand>>());
            Assert.True(this.container.IsRegistered<IResponseCommandValidation<ComplexResponseCommand, int>>());
            
            Assert.True(this.container.IsRegistered<IResponseCommandHandler<SimpleResponseCommand, int>>());
            Assert.True(this.container.IsRegistered<IVoidCommandHandler<SimpleVoidCommand>>());
            
            Assert.True(this.container.IsRegistered<IResponseCommandHandler<ComplexResponseCommand, int>>());
            Assert.True(this.container.IsRegistered<IVoidCommandHandler<ComplexVoidCommand>>());
        }
        
        public void ServiceContainerBuilder_UseCqrsInvalidInput_ExpectException()
        {
            Assert.Throws<ArgumentException>(() => this.containerBuilder.UseCqrs());
        }

        public void ServiceContainerBuilder_RegisterTypeAsSingleton_ExpectInstances()
        {
            this.containerBuilder.RegisterTypeAsSingleton<SomeClass, ISomeInterface>();
            this.BuildContainer();
            
            Assert.True(this.container.IsRegistered<ISomeInterface>());

            ISomeInterface firstInstance = null;
            ISomeInterface secondInstance = null;

            using (var scope = this.container.BeginLifetimeScope())
            {
                firstInstance = scope.Resolve<ISomeInterface>();
            }

            using (var scope = this.container.BeginLifetimeScope())
            {
                secondInstance = scope.Resolve<ISomeInterface>();
            }
            
            Assert.Equal(firstInstance, secondInstance);
        }
        
        [Fact]
        public void ServiceContainerBuilder_RegisterTypePerDependency_ExpectInstances()
        {
            this.containerBuilder.RegisterTypePerDependency<SomeClass, ISomeInterface>();
            this.BuildContainer();
            
            Assert.True(this.container.IsRegistered<ISomeInterface>());

            ISomeInterface firstInstance = null;
            ISomeInterface secondInstance = null;

            using (var scope = this.container.BeginLifetimeScope())
            {
                firstInstance = scope.Resolve<ISomeInterface>();
            }

            using (var scope = this.container.BeginLifetimeScope())
            {
                secondInstance = scope.Resolve<ISomeInterface>();
            }
            
            Assert.NotEqual(firstInstance, secondInstance);
        }
        
        [Fact]
        public void ServiceContainerBuilder_RegisterTypePerLifetimeScope_ExpectInstances()
        {
            this.containerBuilder.RegisterTypePerLifetimeScope<SomeClass, ISomeInterface>();
            this.BuildContainer();
            
            Assert.True(this.container.IsRegistered<ISomeInterface>());

            ISomeInterface firstScopeInstanceOne = null;
            ISomeInterface firstScopeInstanceTwo = null;
            ISomeInterface secondScopeInstanceOne = null;
            ISomeInterface secondScopeInstanceTwo = null;

            using (var scope = this.container.BeginLifetimeScope())
            {
                firstScopeInstanceOne = scope.Resolve<ISomeInterface>();
                firstScopeInstanceTwo = scope.Resolve<ISomeInterface>();
            }

            using (var scope = this.container.BeginLifetimeScope())
            {
                secondScopeInstanceOne = scope.Resolve<ISomeInterface>();
                secondScopeInstanceTwo = scope.Resolve<ISomeInterface>();
            }
            
            Assert.NotEqual(firstScopeInstanceOne, secondScopeInstanceOne);
            Assert.Equal(firstScopeInstanceOne, firstScopeInstanceTwo);
            Assert.Equal(secondScopeInstanceOne, secondScopeInstanceTwo);
        }
        
        [Fact]
        public void ServiceContainerBuilder_RegisterInstance_ExpectInstances()
        {
            this.containerBuilder.RegisterInstance<ISomeInterface>(new SomeClass());
            this.BuildContainer();
            
            Assert.True(this.container.IsRegistered<ISomeInterface>());

            ISomeInterface firstInstance = null;
            ISomeInterface secondInstance = null;

            using (var scope = this.container.BeginLifetimeScope())
            {
                firstInstance = scope.Resolve<ISomeInterface>();
            }

            using (var scope = this.container.BeginLifetimeScope())
            {
                secondInstance = scope.Resolve<ISomeInterface>();
            }
            
            Assert.Equal(firstInstance, secondInstance);
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