namespace Tournamentz.Host
{
    using Autofac;
    using Owin;
    using System.Web.Mvc;

    public partial class Startup
    {
        public void ConfigureAutofac(IAppBuilder app)
        {
            IContainer container = DependencyResolver.Current.GetService<IContainer>();

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}