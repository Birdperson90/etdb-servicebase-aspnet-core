using System;
using System.Threading.Tasks;
using Autofac;
using Etdb.ServiceBase.Builder.Builder;
using Etdb.ServiceBase.Cqrs.Abstractions.Bus;
using Etdb.ServiceBase.Exceptions;
using Etdb.ServiceBase.TestInfrastructure.Cqrs.Commands;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Etdb.ServiceBase.Cqrs.IntegrationTests
{
    public class MediatorHandlerTests : IDisposable
    {
        private readonly IContainer container;
        private readonly IBus mediator;

        public MediatorHandlerTests()
        {
            var serviceContainerBuilder = new ServiceContainerBuilder(new ContainerBuilder());
            serviceContainerBuilder.UseCqrs(typeof(ComplexVoidCommand).Assembly);
            this.container = serviceContainerBuilder.Build(new ServiceCollection());
            this.mediator = this.container.Resolve<IBus>();
        }

        [Fact]
        public async Task MediatorHandler_SendSimpleVoidCommandAsync_ExpectNoError()
        {
            var command = new SimpleVoidCommand();
            await this.mediator.SendCommandAsync(command);

            Assert.Equal(5, command.Value);

            command.Value = 0;
            var task = this.mediator.SendCommandAsync(command);

            Task.WaitAll(task);

            Assert.Equal(5, command.Value);
        }

        [Fact]
        public void MediatorHandler_SendSimpleVoidCommandSync_ExpectNoError()
        {
            var command = new SimpleVoidCommand();

            var task = this.mediator.SendCommand(command);

            Task.WaitAll(task);

            Assert.Equal(5, command.Value);
        }

        [Fact]
        public async Task MediatorHandler_SendSimpleResponseCommandAsync_ExpectNoError()
        {
            var command = new SimpleResponseCommand();

            var result = await this.mediator.SendCommandAsync<SimpleResponseCommand, int>(command);

            Assert.Equal(5, command.Value);
            Assert.Equal(10, result);

            command.Value = 22;

            result = this.mediator.SendCommandAsync<SimpleResponseCommand, int>(command).Result;

            Assert.Equal(5, command.Value);
            Assert.Equal(10, result);
        }

        [Fact]
        public void MediatorHandler_SendSimpleResponseCommandSync_ExpectNoError()
        {
            var command = new SimpleResponseCommand();

            var result = this.mediator.SendCommand<SimpleResponseCommand, int>(command).Result;

            Assert.Equal(5, command.Value);
            Assert.Equal(10, result);

            command.Value = 22;

            result = this.mediator.SendCommand<SimpleResponseCommand, int>(command).Result;

            Assert.Equal(5, command.Value);
            Assert.Equal(10, result);
        }

        [Fact]
        public async Task MediatorHandler_SendComplexVoidCommandAsync_ExpectNoError()
        {
            var command = new ComplexVoidCommand();

            await this.mediator.SendCommandAsync(command);

            Assert.Equal(5, command.Value);

            command.Value = 0;

            var task = this.mediator.SendCommandAsync(command);

            Task.WaitAll(task);

            Assert.Equal(5, command.Value);
        }

        [Fact]
        public void MediatorHandler_SendComplexVoidCommandSync_ExpectNoError()
        {
            var command = new ComplexVoidCommand();

            var task = this.mediator.SendCommand(command);

            Task.WaitAll(task);

            Assert.Equal(5, command.Value);

            command.Value = 0;

            task = this.mediator.SendCommand(command);

            Task.WaitAll(task);

            Assert.Equal(5, command.Value);
        }

        [Fact]
        public async Task MediatorHandler_SendComplexResponseCommandAsync_ExpectNoError()
        {
            var command = new ComplexResponseCommand();

            var result = await this.mediator.SendCommandAsync<ComplexResponseCommand, int>(command);

            Assert.Equal(5, command.Value);
            Assert.Equal(10, result);

            command.Value = 0;

            result = this.mediator.SendCommandAsync<ComplexResponseCommand, int>(command).Result;

            Assert.Equal(5, command.Value);
            Assert.Equal(10, result);
        }

        [Fact]
        public void MediatorHandler_SendComplexResponseCommandSync_ExpectNoError()
        {
            var command = new ComplexResponseCommand();

            var result = this.mediator.SendCommand<ComplexResponseCommand, int>(command).Result;

            Assert.Equal(5, command.Value);
            Assert.Equal(10, result);

            command.Value = 0;

            result = this.mediator.SendCommand<ComplexResponseCommand, int>(command).Result;

            Assert.Equal(5, command.Value);
            Assert.Equal(10, result);
        }

        [Fact]
        public async Task MediatorHandler_SendComplexVoidCommandAsyncInvalidInput_ExpectValidationException()
        {
            var command = new ComplexVoidCommand
            {
                Value = 5
            };

            await Assert.ThrowsAsync<GeneralValidationException>(() => this.mediator.SendCommandAsync(command));
        }

        [Fact]
        public async Task MediatorHandler_SendComplexVoidCommandSyncInvalidInput_ExpectValidationException()
        {
            var command = new ComplexVoidCommand
            {
                Value = 5
            };

            await Assert.ThrowsAsync<GeneralValidationException>(() => this.mediator.SendCommand(command));
        }

        [Fact]
        public async Task MediatorHandler_SendComplexResponseCommandAsyncInvalidInput_ExpectValidationException()
        {
            var command = new ComplexResponseCommand
            {
                Value = 5
            };

            await Assert.ThrowsAsync<GeneralValidationException>(() =>
                this.mediator.SendCommandAsync<ComplexResponseCommand, int>(command));
        }

        [Fact]
        public async Task MediatorHandler_SendComplexResponseCommandSyncInvalidInput_ExpectValidationException()
        {
            var command = new ComplexResponseCommand
            {
                Value = 5
            };

            await Assert.ThrowsAsync<GeneralValidationException>(() =>
                this.mediator.SendCommand<ComplexResponseCommand, int>(command));
        }

        public void Dispose()
        {
            this.container?.Dispose();
        }
    }
}