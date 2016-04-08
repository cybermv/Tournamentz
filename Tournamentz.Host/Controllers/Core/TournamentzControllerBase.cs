namespace Tournamentz.Host.Controllers.Core
{
    using BL.Core;
    using DAL.Core;
    using DAL.Entity;
    using Microsoft.Owin;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;

    public abstract class TournamentzControllerBase : Controller
    {
        protected TournamentzControllerBase(IExecutionContext executionContext)
        {
            this.ExecutionContext = executionContext;
        }

        public IExecutionContext ExecutionContext { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            IOwinContext owinContext = filterContext.HttpContext.GetOwinContext();

            IRepository<ApplicationUser> userRepo = this.ExecutionContext.UnitOfWork
                .Repository<ApplicationUser>();

            // TODO: set User property

            //Guid userId = owinContext.Authentication.User.Identity.GetUserId();

            //controller.ExecutionContext.User = userRepo.FindById(userId);
        }

        protected override void OnAuthentication(AuthenticationContext filterContext)
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
        }

        protected override void Dispose(bool disposing)
        {
            this.ExecutionContext.Dispose();
            base.Dispose(disposing);
        }
    }
}