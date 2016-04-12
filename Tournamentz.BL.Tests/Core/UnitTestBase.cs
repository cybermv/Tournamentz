namespace Tournamentz.BL.Tests.Core
{
    using System;
    using System.Reflection;
    using Autofac;
    using BL.Core;
    using BL.Core.Command;
    using BL.Core.Command.Interface;
    using DAL.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public abstract class UnitTestBase : IDisposable
    {
        protected UnitTestBase()
        {
            this.ExecutionContext = TournamentzContainerProvider.Instance.Resolve<IExecutionContext>();
        }

        public IExecutionContext ExecutionContext { get; private set; }

        public IUnitOfWork UnitOfWork { get { return this.ExecutionContext.UnitOfWork; } }

        protected TResult RunAsPrerequisite<TResult>(ICommand command)
        {
            command.ExecutionContext = this.ExecutionContext;
            TResult result;

            try
            {
                ICommandResult commandResult = (ICommandResult) this
                    .GetType()
                    .GetMethod("RunCommand", BindingFlags.Instance | BindingFlags.NonPublic)
                    .MakeGenericMethod(command.GetType())
                    .Invoke(this, new object[] {command});

                if (commandResult.Status != CommandResultStatus.Success)
                {
                    throw new Exception("Prerequisite command execution failed with the following error: " + commandResult);
                }

                result = (TResult) commandResult.ReturnValue;
            }
            catch (Exception ex)
            {
                Assert.Inconclusive(ex.ToString());
                throw; // unreachable, but C# doesn't know that :P
            }

            return result;
        }

        protected ICommandResult RunCommand<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            command.ExecutionContext = this.ExecutionContext;

            ICommandGate<TCommand> commandGate = this.ExecutionContext.Services
                .Resolve<ICommandGate<TCommand>>();

            return commandGate.Run(command);
        }

        public void Dispose()
        {
            this.ExecutionContext.UnitOfWork.Rollback();
            this.ExecutionContext.Dispose();
        }
    }
}