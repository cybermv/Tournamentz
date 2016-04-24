namespace Tournamentz.Host.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI.WebControls;
    using Autofac;
    using BL.Commands;
    using BL.Core;
    using BL.Core.Command;
    using BL.Core.Command.Interface;
    using BL.Core.Query.Interface;
    using BL.Queries;
    using Core;

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
            return View();
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
        public ActionResult ById(Guid id)
        {
            IQueryResult<TeamQueries.My> queryResult = this.RunQuery<TeamQueries.My>();
            TeamQueries.My team = queryResult.Query
                .SingleOrDefault(t => t.Id == id);
            return View(team);
        }

        // POST: Teams/New
        public ActionResult New(TeamCommands.Create createCommand)
        {
            ICommandResult commandResult = this.RunCommand(createCommand);

            if (commandResult.IsFailed())
            {
                return View(createCommand);
            }

            return RedirectToAction("ById", new {id = commandResult.ReturnValue});
        }

    }
}