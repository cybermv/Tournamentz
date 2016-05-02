namespace Tournamentz.Host.Controllers
{
    using BL;
    using BL.Commands;
    using BL.Core;
    using BL.Core.Command.Interface;
    using BL.Core.Query.Interface;
    using BL.Queries;
    using Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

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

        // GET: Tournaments/ById/b35a3870-6b50-4aec-ae4f-82f4a0fedf13
        [HttpGet]
        public ActionResult ById(Guid? id)
        {
            if (!id.HasValue) { return this.RedirectToAction("My", "Tournaments"); }

            IQueryResult<TournamentQueries.My> myTournaments = this.RunQuery<TournamentQueries.My>();

            TournamentQueries.My tournamentById = myTournaments.Query
                .SingleOrDefault(t => t.Id == id.Value);

            if (tournamentById == null) { return RedirectToAction("My", "Tournaments"); }

            IQueryResult<TeamQueries.All> allTeams = this.RunQuery<TeamQueries.All>();
            List<TeamQueries.All> allTeamsList = allTeams.Query.ToList();
            ViewBag.Teams = new SelectList(allTeamsList, "Id", "Text");

            return View(tournamentById);
        }

        // GET: Tournaments/New
        [HttpGet]
        public ActionResult New()
        {
            IQueryResult<TournamentQueries.Types> types = this.RunQuery<TournamentQueries.Types>();
            List<TournamentQueries.Types> typeList = types.Query.ToList();

            ViewBag.TournamentTypes = new SelectList(typeList, "Id", "Text");

            TournamentCommands.Create createCommand = new TournamentCommands.Create
            {
                Title = string.Format("Turnir {0}", this.RunQuery<TournamentQueries.My>().Query.Count() + 1)
            };
            return View(createCommand);
        }

        // POST: Tournaments/New
        [HttpPost]
        public ActionResult New(TournamentCommands.Create createCommand)
        {
            ICommandResult commandResult = this.RunCommand(createCommand);

            if (commandResult.IsFailed())
            {
                this.AddErrorsFromResult(commandResult);
                return View(createCommand);
            }

            return RedirectToAction("ById", new { id = commandResult.ReturnValue });
        }

        // GET: Tournaments/Teams/b35a3870-6b50-4aec-ae4f-82f4a0fedf13
        [HttpGet]
        public ActionResult Teams(Guid? id)
        {
            if (!id.HasValue) { return this.StatusCode(HttpStatusCode.NotFound); }

            IQueryResult<TournamentQueries.My> queryResult = this.RunQuery<TournamentQueries.My>();

            if (queryResult.IsFailed())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TournamentQueries.My tournament = queryResult.Query
                .SingleOrDefault(t => t.Id == id);

            if (tournament == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return this.Json(tournament.Teams, JsonRequestBehavior.AllowGet);
        }

        // POST: Tournaments/AddTeam
        [HttpPost]
        public ActionResult AddTeam(TournamentCommands.AddTeam command)
        {
            ICommandResult result = this.RunCommand(command);

            if (result.IsFailed())
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.Json(result.BusinessRules.Where(b => b.IsBroken));
            }

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.Json(new { ReturnValue = result.ReturnValue });
        }

        // POST: Tournaments/RemoveTeam
        [HttpPost]
        public ActionResult RemoveTeam(TournamentCommands.RemoveTeam command)
        {
            ICommandResult result = this.RunCommand(command);

            if (result.IsFailed())
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.Json(result.BusinessRules.Where(b => b.IsBroken));
            }

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.Json(new { ReturnValue = result.ReturnValue });
        }
    }
}