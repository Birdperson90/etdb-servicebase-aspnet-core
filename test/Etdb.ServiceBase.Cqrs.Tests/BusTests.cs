using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.FluentBuilder;
using Etdb.ServiceBase.Cqrs.Abstractions.Bus;
using Etdb.ServiceBase.Cqrs.Abstractions.Validation;
using Etdb.ServiceBase.Exceptions;
using Etdb.ServiceBase.TestInfrastructure.Cqrs.Commands;
using Etdb.ServiceBase.TestInfrastructure.Cqrs.Validation;
using MediatR.Extensions.Autofac.DependencyInjection;
using Xunit;

namespace Etdb.ServiceBase.Cqrs.Tests
{
    public class BusTests : IDisposable
    {
        private readonly IContainer container;
        private readonly IBus bus;

        public BusTests()
        {
            this.container =
                new AutofacFluentBuilder(new ContainerBuilder().AddMediatR(typeof(SimpleVoidCommand).Assembly))
                    .AddClosedTypeAsScoped(typeof(ICommandValidation<>),
                        new[] {typeof(ComplexCommandValidation).Assembly})
                    .RegisterTypeAsScoped<Bus.Bus, IBus>()
                    .Build();

            this.bus = this.container.Resolve<IBus>();
        }

        [Fact]
        public async Task Bus_SendSimpleVoidCommandAsync_ExpectNoError()
        {
            var command = new SimpleVoidCommand();
            await this.bus.SendCommandAsync(command);

            Assert.Equal(5, command.Value);

            command.Value = 0;
            var task = this.bus.SendCommandAsync(command);

            Task.WaitAll(task);

            Assert.Equal(5, command.Value);
        }

        [Fact]
        public async Task Bus_SendSimpleResponseCommandAsync_ExpectNoError()
        {
            var command = new SimpleResponseCommand();

            var result = await this.bus.SendCommandAsync<SimpleResponseCommand, int>(command);

            Assert.Equal(5, command.Value);
            Assert.Equal(10, result);

            command.Value = 22;

            result = this.bus.SendCommandAsync<SimpleResponseCommand, int>(command).Result;

            Assert.Equal(5, command.Value);
            Assert.Equal(10, result);
        }

        [Fact]
        public async Task Bus_SendComplexVoidCommandAsync_ExpectNoError()
        {
            var command = new ComplexVoidCommand();

            await this.bus.SendCommandAsync(command);

            Assert.Equal(5, command.Value);

            command.Value = 0;

            var task = this.bus.SendCommandAsync(command);

            Task.WaitAll(task);

            Assert.Equal(5, command.Value);
        }

        [Fact]
        public async Task Bus_SendComplexResponseCommandAsync_ExpectNoError()
        {
            var command = new ComplexResponseCommand();

            var result = await this.bus.SendCommandAsync<ComplexResponseCommand, int>(command);

            Assert.Equal(5, command.Value);
            Assert.Equal(10, result);

            command.Value = 0;

            result = this.bus.SendCommandAsync<ComplexResponseCommand, int>(command).Result;

            Assert.Equal(5, command.Value);
            Assert.Equal(10, result);
        }

        [Fact]
        public async Task Bus_SendComplexVoidCommandAsyncInvalidInput_ExpectValidationException()
        {
            var command = new ComplexVoidCommand
            {
                Value = 5
            };

            await Assert.ThrowsAsync<GeneralValidationException>(() => this.bus.SendCommandAsync(command));
        }

        [Fact]
        public async Task Bus_SendComplexResponseCommandAsyncInvalidInput_ExpectValidationException()
        {
            var command = new ComplexResponseCommand
            {
                Value = 5
            };

            await Assert.ThrowsAsync<GeneralValidationException>(() =>
                this.bus.SendCommandAsync<ComplexResponseCommand, int>(command));
        }

        public void Dispose()
        {
            this.container?.Dispose();
        }
    }
}