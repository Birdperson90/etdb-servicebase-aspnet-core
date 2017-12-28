using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Etdb.ServiceBase.Builder.Modules;
using Etdb.ServiceBase.EventSourcing.Abstractions.Base;
using Etdb.ServiceBase.EventSourcing.Abstractions.Bus;
using Etdb.ServiceBase.EventSourcing.Abstractions.Handler;
using Etdb.ServiceBase.EventSourcing.Abstractions.Repositories;
using Etdb.ServiceBase.EventSourcing.Abstractions.Validation;
using Etdb.ServiceBase.EventSourcing.Base;
using Etdb.ServiceBase.EventSourcing.Mediator;
using Etdb.ServiceBase.EventSourcing.Repositories;
using Etdb.ServiceBase.Repositories.Abstractions.Base;
using Etdb.ServiceBase.Repositories.Generics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Etdb.ServiceBase.Builder.Builder
{
    public class ServiceContainerBuilder
    {
        private readonly ContainerBuilder containerBuilder;

        public ServiceContainerBuilder(ContainerBuilder containerBuilder)
        {
            this.containerBuilder = containerBuilder;
        }

        public ServiceContainerBuilder UseConfiguration(IConfigurationRoot configurationRoot)
        {
            this.containerBuilder
                .RegisterInstance(configurationRoot)
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

        public ServiceContainerBuilder UseGenericRepositoryPattern<TDbContext>(params Assembly[] assembliesToScan) where TDbContext : AppContextBase
        {
            if (!assembliesToScan.Any())
            {
                throw new ArgumentException(@"You need to provide assemblies in order to implement generic 
                    Repositories!", nameof(assembliesToScan));
            }

            this.containerBuilder.RegisterAssemblyTypes(assembliesToScan)
                .AsClosedTypesOf(typeof(EntityRepository<>))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope()
                .WithParameter(new ResolvedParameter(
                    (parameterInfo, componentContext) => parameterInfo.ParameterType == typeof(AppContextBase),
                    (parameterInfo, componentContext) => componentContext.Resolve<TDbContext>()))
                .InstancePerLifetimeScope();

            return this;
        }

        public ServiceContainerBuilder UseEventSourcing<TAppDbContext, TEventDbContext>(params Assembly[] assembliesToScan)
            where TAppDbContext : AppContextBase where TEventDbContext : EventStoreContextBase
        {
            if (!assembliesToScan.Any())
            {
                throw new ArgumentException(@"You need to provide assemblies in order to implement generic 
                    Command- and Domaineventhandlers!", nameof(assembliesToScan));
            }

            this.containerBuilder.RegisterModule(
                new EventSourcingModule<TAppDbContext, TEventDbContext>(assembliesToScan));

            return this;
        }

        public ServiceContainerBuilder RegisterTypeAsSingleton<TImplementation>()
        {
            this.containerBuilder.RegisterType<TImplementation>()
                .SingleInstance();
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

        public ServiceContainerBuilder RegisterTypePerRequest<TImplementation, TInterface>()
            where TImplementation : TInterface
        {
            this.containerBuilder.RegisterType<TImplementation>()
                .As<TInterface>()
                .InstancePerRequest();

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

        public IContainer Build(IServiceCollection serviceCollection)
        {
            this.containerBuilder.Populate(serviceCollection);
            return this.containerBuilder
                .Build();
        }
    }
}
