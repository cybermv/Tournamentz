namespace Tournamentz.Host.Api.Controllers.Core
{
    using System;
    using System.Net.Http;
    using Extensions;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using DAL.Entity;
    using DAL.Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin;

    public class HandleUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            TournamentzApiControllerBase controller = actionContext.ControllerContext.GetTournamentzController();

            IOwinContext owinContext = actionContext.Request.GetOwinContext();
            
            if (owinContext.Authentication.User?.Identity != null &&
                owinContext.Authentication.User.Identity.IsAuthenticated)
            {
                Guid userId = owinContext.Authentication.User.Identity.GetUserGuid();

                ApplicationUserManager userManager = new ApplicationUserManager(controller.ExecutionContext.UnitOfWork);
                ApplicationUser user = userManager.FindById(userId);

                controller.ExecutionContext.User = user;

                if (controller.ExecutionContext.User == null)
                {
                    throw new Exception("The currently logged in user is not present in the database");
                }
            }


            
        }
    }
}