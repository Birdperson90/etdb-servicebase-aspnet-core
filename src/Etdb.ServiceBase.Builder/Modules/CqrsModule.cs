using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Etdb.ServiceBase.Cqrs.Abstractions.Bus;
using Etdb.ServiceBase.Cqrs.Abstractions.Validation;
using Etdb.ServiceBase.Cqrs.Bus;
using MediatR;
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
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
                .AsImplementedInterfaces();

            builder.RegisterType<MediatorHandler>()
                .As<IMediatorHandler>()
                .AsSelf()
                .InstancePerLifetimeScope();

            var openGenericTypes = new[]
            {
                typeof(IRequestHandler<>),
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
                typeof(IResponseCommandValidation<,>),
                typeof(IVoidCommandValidation<>),
            };

            foreach (var openGenericType in openGenericTypes)
            {
                builder.RegisterAssemblyTypes(this.assembliesToScan)
                    .AsClosedTypesOf(openGenericType)
                    .AsImplementedInterfaces();
            }

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var componentContext = ctx.Resolve<IComponentContext>();

                return type => componentContext.Resolve(type);
            });

            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var componentContext = ctx.Resolve<IComponentContext>();

                return type => (IEnumerable<object>) componentContext
                    .Resolve(typeof(IEnumerable<>)
                        .MakeGenericType(type));
            });
        }
    }
}