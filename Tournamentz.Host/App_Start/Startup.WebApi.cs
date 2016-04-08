namespace Tournamentz.Host
{
    using Autofac;
    using Autofac.Integration.WebApi;
    using Owin;
    using System.Web.Http;
    using System.Web.Mvc;

    public partial class Startup
    {
        public static void ConfigureWebApi(IAppBuilder app)
        {
            IContainer container = DependencyResolver.Current.GetService<IContainer>();

            HttpConfiguration config = new HttpConfiguration
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(container)
            };

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
}