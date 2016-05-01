namespace Tournamentz.Host.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using BL;
    using BL.Core;
    using BL.Core.Query.Interface;
    using BL.Queries;
    using Core;

    [Authorize]
    public class TournamentsController : TournamentzControllerBase
    {
        public TournamentsController(IExecutionContext executionContext)
            : base(executionContext)
        {
        }

        // GET: Tournaments
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("My");
        }

        // GET: Tournaments/My
        [HttpGet]
        public ActionResult My()
        {
            IQueryResult<TournamentQueries.My> myTournaments = this.RunQuery<TournamentQueries.My>();

            List<TournamentQueries.My> myTournamentsList = myTournaments.Query.ToList();

            return View(myTournamentsList);
        }

        // GET: Tournaments/All
        [HttpGet, Authorize(Roles = TournamentzRoles.AdminText)]
        public ActionResult All()
        {
            IQueryResult<TournamentQueries.All> allTournaments = this.RunQuery<TournamentQueries.All>();

            List<TournamentQueries.All> allTournamentsList = allTournaments.Query.ToList();

            return View(allTournamentsList);
        }
    }
}