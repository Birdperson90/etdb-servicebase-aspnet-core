using System;
using System.Reflection;
using Autofac;
using Etdb.ServiceBase.Cqrs.Abstractions.Bus;
using Etdb.ServiceBase.Cqrs.Abstractions.Validation;
using Etdb.ServiceBase.Cqrs.Bus;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Pipeline;

namespace Etdb.ServiceBase.Builder.Modules
{
    internal class CqrsModule : Autofac.Module
    {
        private readonly Assembly[] assembliesToScan;

        public CqrsModule(Assembly[] assembliesToScan)
        {
            this.assembliesToScan = assembliesToScan ?? throw new ArgumentNullException(nameof(assembliesToScan));
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.AddMediatR(this.assembliesToScan);
            
            builder.RegisterType<Bus>()
                .As<IBus>()
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(this.assembliesToScan)
                .AsClosedTypesOf(typeof(ICommandValidation<>))
                .AsImplementedInterfaces();
        }
    }
}