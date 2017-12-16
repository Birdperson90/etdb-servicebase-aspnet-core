using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Etdb.ServiceBase.EventSourcing.Abstractions.Base;
using Etdb.ServiceBase.EventSourcing.Abstractions.Bus;
using Etdb.ServiceBase.EventSourcing.Abstractions.Handler;
using Etdb.ServiceBase.EventSourcing.Abstractions.Repositories;
using Etdb.ServiceBase.EventSourcing.Base;
using Etdb.ServiceBase.EventSourcing.Handler;
using Etdb.ServiceBase.EventSourcing.Mediator;
using Etdb.ServiceBase.EventSourcing.Repositories;
using Etdb.ServiceBase.EventSourcing.Validation;
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

        public ServiceContainerBuilder UseEventSourcing<TAppDbContext, TEventStoreDbContext>(params Assembly[] assembliesToScan)
            where TAppDbContext : AppContextBase where TEventStoreDbContext : EventStoreContextBase
        {
            if (!assembliesToScan.Any())
            {
                throw new ArgumentException(@"You need to provide assemblies in order to implement generic 
                    Command- and Domaineventhandlers!", nameof(assembliesToScan));
            }

            this.containerBuilder.RegisterType<TAppDbContext>()
                .InstancePerLifetimeScope();

            this.containerBuilder.RegisterType<TEventStoreDbContext>()
                .InstancePerLifetimeScope();

            this.containerBuilder.RegisterAssemblyTypes(assembliesToScan)
                .AsClosedTypesOf(typeof(CommandValidation<>))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope();

            this.containerBuilder.RegisterAssemblyTypes(assembliesToScan)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .AsSelf()
                .InstancePerLifetimeScope();

            this.containerBuilder.RegisterAssemblyTypes(assembliesToScan)
                .AsClosedTypesOf(typeof(IDomainEventHandler<>))
                .AsSelf()
                .InstancePerLifetimeScope();

            this.containerBuilder.RegisterGeneric(typeof(DomainNotificationHandler<>))
                .As(typeof(IDomainNotificationHandler<>))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope();

            this.containerBuilder.RegisterType<MediatorHandler>()
                .As<IMediatorHandler>()
                .AsSelf()
                .InstancePerLifetimeScope();

            this.containerBuilder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .AsSelf()
                .InstancePerLifetimeScope()
                .WithParameter(new ResolvedParameter(
                    (parameterInfo, componentContext) => parameterInfo.ParameterType == typeof(AppContextBase),
                    (parameterInfo, componentContext) => componentContext.Resolve<TAppDbContext>()))
                .InstancePerLifetimeScope();

            this.containerBuilder.RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>()
                .AsSelf()
                .InstancePerLifetimeScope();

            this.containerBuilder.RegisterType<EventStoreRepository>()
                .As<IEventStoreRepository>()
                .AsSelf()
                .InstancePerLifetimeScope()
                .WithParameter(new ResolvedParameter(
                    (parameterInfo, componentContext) => parameterInfo.ParameterType == typeof(EventStoreContextBase),
                    (parameterInfo, componentContext) => componentContext.Resolve<TEventStoreDbContext>()))
                .InstancePerLifetimeScope();

            this.containerBuilder.RegisterType<EventUser>()
                .As<IEventUser>()
                .AsSelf()
                .InstancePerLifetimeScope();

            this.containerBuilder.RegisterType<EventStore>()
                .As<IEventStore>()
                .AsSelf()
                .InstancePerLifetimeScope();

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
