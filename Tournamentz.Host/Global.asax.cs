namespace Tournamentz.Host
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using BL.Core;
    using DAL;
    using DAL.Core;
    using System.Data.Entity;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IContainer container = DependencyInjectionConfig.BuildContainer();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}