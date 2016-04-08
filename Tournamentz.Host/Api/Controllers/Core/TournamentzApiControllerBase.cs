namespace Tournamentz.Host.Api.Controllers.Core
{
    using BL.Core;
    using System.Web.Http;

    [HandleUser]
    public class TournamentzApiControllerBase : ApiController
    {
        protected TournamentzApiControllerBase(IExecutionContext executionContext)
        {
            this.ExecutionContext = executionContext;
        }

        public IExecutionContext ExecutionContext { get; private set; }

        protected override void Dispose(bool disposing)
        {
            this.ExecutionContext.Dispose();
            base.Dispose(disposing);
        }
    }
}