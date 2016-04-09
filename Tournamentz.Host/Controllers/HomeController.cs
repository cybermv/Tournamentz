namespace Tournamentz.Host.Controllers
{
    using BL.Core;
    using Core;
    using System.Web.Mvc;

    public class HomeController : TournamentzControllerBase
    {
        public HomeController(IExecutionContext executionContext)
            : base(executionContext)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}