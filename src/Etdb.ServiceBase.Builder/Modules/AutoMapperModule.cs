using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AutoMapper;
using AutoMapper.EquivalencyExpression;

namespace Etdb.ServiceBase.Builder.Modules
{
    internal class AutoMapperModule : Autofac.Module
    {
        private readonly Assembly[] assembliesToScan;

        public AutoMapperModule(Assembly[] assembliesToScan)
        {
            this.assembliesToScan = assembliesToScan ?? throw new ArgumentNullException(nameof(assembliesToScan));
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(this.assembliesToScan)
                .AssignableTo(typeof(Profile))
                .As<Profile>()
                .SingleInstance();

            builder
                .Register(componentContext => new MapperConfiguration(config =>
                {
                    var profiles = componentContext.Resolve<IEnumerable<Profile>>();

                    foreach (var profile in profiles)
                    {
                        config.AddProfile(profile.GetType());
                    }

                    config.AddCollectionMappers();
                }))
                .AsSelf()
                .SingleInstance();

            var openTypes = new[]
            {
                typeof(IValueResolver<,,>),
                typeof(IMemberValueResolver<,,,>),
                typeof(ITypeConverter<,>),
                typeof(IMappingAction<,>)
            };

            foreach (var openType in openTypes)
            {
                builder.RegisterAssemblyTypes(this.assembliesToScan)
                    .AsClosedTypesOf(openType)
                    .AsImplementedInterfaces()
                    .InstancePerDependency();
            }

            builder
                .Register(componentContext => componentContext
                    .Resolve<MapperConfiguration>()
                    .CreateMapper(componentContext.Resolve<IComponentContext>().Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}