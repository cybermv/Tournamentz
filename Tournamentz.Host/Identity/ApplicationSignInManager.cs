namespace Tournamentz.Host.Identity
{
    using DAL.Entity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ApplicationSignInManager : SignInManager<ApplicationUser, Guid>
    {
        public ApplicationSignInManager(DAL.Identity.ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((DAL.Identity.ApplicationUserManager)this.UserManager);
        }
    }
}