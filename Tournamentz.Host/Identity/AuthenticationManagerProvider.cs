namespace Tournamentz.Host.Identity
{
    using Autofac;
    using Microsoft.Owin.Security;
    using System.Web;

    public class AuthenticationManagerProvider
    {
        public static IAuthenticationManager Resolve(IComponentContext context)
        {
            return HttpContext.Current.GetOwinContext().Authentication;
        }
    }
}