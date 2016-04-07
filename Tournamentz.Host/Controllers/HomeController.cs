namespace Tournamentz.Host.Controllers
{
    using BL.Core;
    using Core;
    using DAL;
    using DAL.Core;
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

            IExecutionContext ex1 = DependencyResolver.Current.GetService<IExecutionContext>();
            IUnitOfWork uow1 = DependencyResolver.Current.GetService<IUnitOfWork>();
            TournamentzModelContext db1 = DependencyResolver.Current.GetService<TournamentzModelContext>();

            if (ex1 != this.ExecutionContext)
            {
                throw new System.Exception("EX nisu isti!");
            }

            if (uow1 == this.ExecutionContext.UnitOfWork)
            {
                throw new System.Exception("UOW SU isti!");
            }

            if (db1 == this.ExecutionContext.UnitOfWork.Context)
            {
                throw new System.Exception("CONTEXT SU isti INSTANCE!");
            }

            uow1.Dispose();
            db1.Dispose();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}