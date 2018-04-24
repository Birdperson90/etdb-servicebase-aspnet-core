using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Etdb.ServiceBase.Builder.Modules;
using Etdb.ServiceBase.DocumentRepository.Abstractions.Context;
using Etdb.ServiceBase.DocumentRepository.Abstractions.Generics;
using Etdb.ServiceBase.EntityRepository.Abstractions.Context;
using Etdb.ServiceBase.EntityRepository.Abstractions.Generics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Etdb.ServiceBase.Builder.Builder
{
    public class ServiceContainerBuilder
    {
        private readonly ContainerBuilder containerBuilder;

        public ServiceContainerBuilder(ContainerBuilder containerBuilder = null)
        {
            this.containerBuilder = containerBuilder ?? new ContainerBuilder();
        }

        public ServiceContainerBuilder UseConfiguration(IConfigurationRoot configurationRoot)
        {
            this.containerBuilder
                .RegisterInstance(configurationRoot)
                .As<IConfigurationRoot>()
                .AsImplementedInterfaces()
                .SingleInstance();
            return this;
        }

        public ServiceContainerBuilder UseEnvironment(IHostingEnvironment hostingEnvironment)
        {
            this.containerBuilder
                .RegisterInstance(hostingEnvironment)
                .SingleInstance();
            return this;
        }

        public ServiceContainerBuilder UseGenericDocumentRepositoryPattern<TDocumentDbContext>(params Assembly[] assembliesToScan) 
            where TDocumentDbContext : DocumentDbContext
        {
            if (!assembliesToScan.Any())
            {
                throw new ArgumentException(@"You need to provide assemblies in order to implement generic 
                    Repositories!", nameof(assembliesToScan));
            }

            this.containerBuilder.RegisterType<TDocumentDbContext>()
                .InstancePerLifetimeScope();

            this.containerBuilder.RegisterAssemblyTypes(assembliesToScan)
                .AsClosedTypesOf(typeof(IDocumentRepository<,>))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope()
                .WithParameter(new ResolvedParameter(
                    (parameterInfo, componentContext) => parameterInfo.ParameterType == typeof(DocumentDbContext),
                    (parameterInfo, componentContext) => componentContext.Resolve<TDocumentDbContext>()));

            return this;
        }

        public ServiceContainerBuilder UseGenericEntityRepositoryPattern<TEntityDbContext>(
            params Assembly[] assembliesToScan) where TEntityDbContext : EntityDbContext
        {
            if (!assembliesToScan.Any())
            {
                throw new ArgumentException(@"You need to provide assemblies in order to implement generic 
                    Repositories!", nameof(assembliesToScan));
            }

            this.containerBuilder.RegisterType<TEntityDbContext>()
                .InstancePerLifetimeScope();

            this.containerBuilder.RegisterAssemblyTypes(assembliesToScan)
                .AsClosedTypesOf(typeof(IEntityRepository<,>))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope()
                .WithParameter(new ResolvedParameter(
                    (parameterInfo, componentContext) => parameterInfo.ParameterType == typeof(EntityDbContext),
                    (parameterInfo, componentContext) => componentContext.Resolve<TEntityDbContext>()));

            return this;
        }

        public ServiceContainerBuilder UseCqrs(params Assembly[] assembliesToScan)
        {
            if (!assembliesToScan.Any())
            {
                throw new ArgumentException(@"You need to provide assemblies in order to implement generic 
                    Command- and Domaineventhandlers!", nameof(assembliesToScan));
            }

            this.containerBuilder.RegisterModule(new CqrsModule(assembliesToScan));

            return this;
        }

        public ServiceContainerBuilder UseAutoMapper(params Assembly[] assembliesToScan)
        {
            if (!assembliesToScan.Any())
            {
                throw new ArgumentException(@"You need to provide assemblies in order to implement generic 
                    Command- and Domaineventhandlers!", nameof(assembliesToScan));
            }

            this.containerBuilder.RegisterModule(new AutoMapperModule(assembliesToScan));

            return this;
        }

        public ServiceContainerBuilder RegisterTypeAsSingleton<TImplementation, TInterface>()
            where TImplementation : TInterface
        {
            this.containerBuilder.RegisterType<TImplementation>()
                .As<TInterface>()
                .AsSelf()
                .SingleInstance();

            return this;
        }

        public ServiceContainerBuilder RegisterTypePerDependency<TImplementation, TInterface>()
            where TImplementation : TInterface
        {
            this.containerBuilder.RegisterType<TImplementation>()
                .As<TInterface>()
                .InstancePerDependency();

            return this;
        }

        public ServiceContainerBuilder RegisterTypePerLifetimeScope<TImplementation, TInterface>()
            where TImplementation : TInterface
        {
            this.containerBuilder.RegisterType<TImplementation>()
                .As<TInterface>()
                .InstancePerLifetimeScope();

            return this;
        }

        public ServiceContainerBuilder RegisterInstance<TType>(object @object) where TType : class
        {
            this.containerBuilder.RegisterInstance(@object)
                .As<TType>()
                .SingleInstance();

            return this;
        }

        public IContainer Build(IServiceCollection services = null)
        {
            this.containerBuilder.Populate(services ?? new ServiceCollection());
            return this.containerBuilder
                .Build();
        }
    }
}
