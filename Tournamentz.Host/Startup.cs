using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tournamentz.Host.Startup))]

namespace Tournamentz.Host
{
    using System.Linq;
    using System.Web.Mvc;
    using DAL.Core;
    using DAL.Entity;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAutofac(app);
            ConfigureAuth(app);
            ConfigureWebApi(app);

            using (IUnitOfWork unitOfWork = DependencyResolver.Current.GetService<IUnitOfWork>())
            {
                if (!unitOfWork.Repository<Player>().Any())
                {
                    TournamentzSeedData.Seed(unitOfWork);

                    unitOfWork.Commit();
                }
            }
        }
    }
}