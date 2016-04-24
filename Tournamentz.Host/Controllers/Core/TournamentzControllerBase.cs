namespace Tournamentz.Host.Controllers.Core
{
    using Autofac;
    using BL.Core;
    using BL.Core.Command.Interface;
    using BL.Core.Query.Interface;
    using BL.Core.Rule;
    using DAL.Core;
    using DAL.Entity;
    using Extensions;
    using Microsoft.Owin;
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using BL.Core.Command;
    using BL.Core.Query;
    using DAL.Identity;
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// Base class for all controllers.
    /// Provides the <see cref="IExecutionContext"/> and methods to run
    /// commands and queries
    /// </summary>
    public abstract class TournamentzControllerBase : Controller
    {
        private bool _disposed;

        protected TournamentzControllerBase(IExecutionContext executionContext)
        {
            this.ExecutionContext = executionContext;
            this._disposed = false;
        }

        /// <summary>
        /// The <see cref="IExecutionContext"/> for the current request
        /// </summary>
        public IExecutionContext ExecutionContext { get; private set; }

        /// <summary>
        /// Runs a given command and returns it's result
        /// </summary>
        /// <typeparam name="TCommand">Type of the command</typeparam>
        /// <param name="command">The command to run</param>
        /// <returns>Execution result</returns>
        protected ICommandResult RunCommand<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            command.ExecutionContext = this.ExecutionContext;

            ICommandGate<TCommand> commandGate = command.ExecutionContext.Services
                .Resolve<ICommandGate<TCommand>>();

            return commandGate.Run(command);
        }

        /// <summary>
        /// Runs a given query and returns the result
        /// </summary>
        /// <typeparam name="TQuery">Type of the query to run</typeparam>
        /// <returns>The query result</returns>
        protected IQueryResult<TQuery> RunQuery<TQuery>()
            where TQuery : class, IQuery, new()
        {
            IQueryGate<TQuery> queryGate = this.ExecutionContext.Services
                .Resolve<IQueryGate<TQuery>>();

            return queryGate.Query(this.ExecutionContext);
        }

        /// <summary>
        /// Adds the errors from the command execution to the <see cref="ModelState"/>
        /// </summary>
        /// <param name="commandResult">Result of the command execution</param>
        protected void AddErrorsFromResult(ICommandResult commandResult)
        {
            foreach (BusinessRule brokenRule in commandResult.BusinessRules.Where(b => b.IsBroken))
            {
                this.ModelState.AddModelError(brokenRule.AffectedProperty, brokenRule.Message);
            }

            foreach (BusinessRule brokenRule in commandResult.PermissionRules.Where(p => p.IsBroken))
            {
                this.ModelState.AddModelError("", brokenRule.Message);
            }

            if (commandResult.Exception != null)
            {
                this.ModelState.AddModelError("", commandResult.Exception);
            }
        }

        /// <summary>
        /// Finds the <see cref="ApplicationUser"/> and sets it on the <see cref="IExecutionContext"/>
        /// </summary>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User?.Identity != null &&
                filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                IOwinContext owinContext = filterContext.HttpContext.GetOwinContext();

                Guid userId = owinContext.Authentication.User.Identity.GetUserGuid();

                ApplicationUserManager userManager = new ApplicationUserManager(this.ExecutionContext.UnitOfWork);
                ApplicationUser user = userManager.FindById(userId);

                this.ExecutionContext.User = user;

                if (this.ExecutionContext.User == null)
                {
                    throw new Exception("The currently logged in user is not present in the database");
                }
            }
        }

        /// <summary>
        /// Handles the disposal of the <see cref="IExecutionContext"/> - either
        /// commits or rollbacks the <see cref="IUnitOfWork"/>
        /// </summary>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (_isCommittedOrRollbacked) { return; }

            int statusCode = filterContext.HttpContext.Response.StatusCode;
            if (statusCode >= 400) // TODO: find better mechanism
            {
                this.Rollback();
            }
            else
            {
                this.Commit();
            }
        }

        private bool _isCommittedOrRollbacked = false;

        protected void Commit()
        {
            if (_isCommittedOrRollbacked) { return; }

            if (_isCommittedOrRollbacked)
            {
                throw new InvalidOperationException("Cannot commit the UnitOfWork more than once");
            }

            _isCommittedOrRollbacked = true;
            this.ExecutionContext.UnitOfWork.Commit();
        }

        protected void Rollback()
        {
            if (_isCommittedOrRollbacked)
            {
                throw new InvalidOperationException("Cannot rollback the UnitOfWork more than once");
            }

            _isCommittedOrRollbacked = true;
            this.ExecutionContext.UnitOfWork.Rollback();
        }

        /*protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            int x;
            x = 4;
            base.OnAuthentication(filterContext);
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            int x;
            x = 4;
            base.OnAuthorization(filterContext);
        }*/

        protected override void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                this.ExecutionContext.Dispose();
                this._disposed = true;
            }

            base.Dispose(disposing);
        }
    }
}