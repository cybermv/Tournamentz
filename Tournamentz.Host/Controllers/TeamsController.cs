namespace Tournamentz.Host.Controllers
{
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
    public class TeamsController : TournamentzControllerBase
    {
        public TeamsController(IExecutionContext executionContext)
            : base(executionContext)
        {
        }

        // GET: Teams
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("My");
        }

        // GET: Teams/My
        [HttpGet]
        public ActionResult My()
        {
            IQueryResult<TeamQueries.My> myTeams = this.RunQuery<TeamQueries.My>();

            List<TeamQueries.My> myTeamsList = myTeams.Query.ToList();

            return View(myTeamsList);
        }

        // GET: Teams/PlayingIn
        [HttpGet]
        public ActionResult PlayingIn()
        {
            IQueryResult<TeamQueries.PlayingIn> allTeams = this.RunQuery<TeamQueries.PlayingIn>();

            List<TeamQueries.PlayingIn> allTeamsList = allTeams.Query.ToList();

            return View(allTeamsList);
        }

        // GET: Teams/All
        [HttpGet]
        public ActionResult All()
        {
            IQueryResult<TeamQueries.All> allTeams = this.RunQuery<TeamQueries.All>();

            List<TeamQueries.All> allTeamsList = allTeams.Query.ToList();

            return View(allTeamsList);
        }

        // GET: Teams/New
        [HttpGet]
        public ActionResult New()
        {
            TeamCommands.Create createCommand = new TeamCommands.Create();
            return View(createCommand);
        }

        // GET: Teams/ById/29d52bfb-d521-4535-b473-3babe914cb96
        [HttpGet]
        public ActionResult ById(Guid? id)
        {
            if (!id.HasValue) { return this.RedirectToAction("My", "Teams");}

            IQueryResult<TeamQueries.All> queryResult = this.RunQuery<TeamQueries.All>();
            TeamQueries.All team = queryResult.Query
                .SingleOrDefault(t => t.Id == id);
            return View(team);
        }

        // POST: Teams/New
        [HttpPost]
        public ActionResult New(TeamCommands.Create createCommand)
        {
            ICommandResult commandResult = this.RunCommand(createCommand);

            if (commandResult.IsFailed())
            {
                this.AddErrorsFromResult(commandResult);
                return View(createCommand);
            }

            return RedirectToAction("ById", new { id = commandResult.ReturnValue });
        }

        // POST: Teams/AddNewPlayer
        [HttpPost]
        public ActionResult AddNewPlayer(TeamCommands.AddNewPlayer command)
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

        // POST: Teams/AddExistingPlayer
        [HttpPost]
        public ActionResult AddExistingPlayer(TeamCommands.AddExistingPlayer command)
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

        // POST: Teams/RemovePlayer
        [HttpPost]
        public ActionResult RemovePlayer(TeamCommands.RemovePlayer command)
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

        // GET: Teams/Players/e4bea7a5-8e36-4d26-a6f2-16291543ab3b
        [HttpGet]
        public ActionResult Players(Guid? id)
        {
            if (!id.HasValue) { return this.StatusCode(HttpStatusCode.NotFound); }

            IQueryResult<TeamQueries.My> queryResult = this.RunQuery<TeamQueries.My>();

            if (queryResult.IsFailed())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TeamQueries.My team = queryResult.Query
                .SingleOrDefault(t => t.Id == id);

            if (team == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return this.Json(team.Players, JsonRequestBehavior.AllowGet);
        }
    }
}