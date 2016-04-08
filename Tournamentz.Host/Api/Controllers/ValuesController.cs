namespace Tournamentz.Host.Api.Controllers
{
    using BL.Core;
    using Core;
    using DAL.Core;
    using DAL.Entity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    [RoutePrefix("api/values")]
    public class ValuesController : TournamentzApiControllerBase
    {
        public ValuesController(IExecutionContext executionContext)
            : base(executionContext)
        {
        }

        [HttpGet, Route("")]
        public IHttpActionResult GetValues()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                IRepository<Player> playerRepo = this.ExecutionContext.UnitOfWork.Repository<Player>();
                List<Player> players = playerRepo.Query.ToList();
                return Ok(players);
            }

            return Unauthorized();
        }
    }
}