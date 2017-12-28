using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Core;
using Etdb.ServiceBase.EventSourcing.Abstractions.Base;
using Etdb.ServiceBase.EventSourcing.Abstractions.Bus;
using Etdb.ServiceBase.EventSourcing.Abstractions.Handler;
using Etdb.ServiceBase.EventSourcing.Abstractions.Repositories;
using Etdb.ServiceBase.EventSourcing.Abstractions.Validation;
using Etdb.ServiceBase.EventSourcing.Base;
using Etdb.ServiceBase.EventSourcing.Mediator;
using Etdb.ServiceBase.EventSourcing.Repositories;
using Etdb.ServiceBase.Repositories.Abstractions.Base;
using Microsoft.AspNetCore.Http;
using Module = Autofac.Module;

namespace Etdb.ServiceBase.Builder.Modules
{
    internal class EventSourcingModule<TAppDbContext, TEventDbContext> : Module
        where TAppDbContext : AppContextBase where TEventDbContext : EventStoreContextBase
    {
        private readonly Assembly[] assembliesToScan;

        public EventSourcingModule(Assembly[] assembliesToScan)
        {
            this.assembliesToScan = assembliesToScan;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TAppDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TEventDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(this.assembliesToScan)
                .AsClosedTypesOf(typeof(ICommandValidation<,>))
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(this.assembliesToScan)
                .AsClosedTypesOf(typeof(ITransactionHandler<,>))
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(this.assembliesToScan)
                .AsClosedTypesOf(typeof(IDomainEventHandler<>))
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<MediatorHandler>()
                .As<IMediatorHandler>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<EventStoreRepository>()
                .As<IEventStoreRepository>()
                .AsSelf()
                .InstancePerLifetimeScope()
                .WithParameter(new ResolvedParameter(
                    (parameterInfo, componentContext) => parameterInfo.ParameterType == typeof(EventStoreContextBase),
                    (parameterInfo, componentContext) => componentContext.Resolve<TEventDbContext>()))
                .InstancePerLifetimeScope();

            builder.RegisterType<EventUser>()
                .As<IEventUser>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<EventStore>()
                .As<IEventStore>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
