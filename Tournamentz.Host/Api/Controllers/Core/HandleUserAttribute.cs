namespace Tournamentz.Host.Api.Controllers.Core
{
    using Extensions;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    public class HandleUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            TournamentzApiControllerBase controller = actionContext.ControllerContext.GetTournamentzController();
        }
    }
}