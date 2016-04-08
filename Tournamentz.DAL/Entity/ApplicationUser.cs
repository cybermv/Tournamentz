namespace Tournamentz.DAL.Entity
{
    using Core;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IUser<Guid>, IEntity
    {
        public virtual Player Player { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, Guid> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // TODO: Add custom user claims here
            return userIdentity;
        }

        public override string ToString()
        {
            return $"Username: {UserName}, player nickname: {Player.Nickname}";
        }
    }

    #region ASP.NET Identity utils

    // TODO: consult this
    // http://www.asp.net/identity/overview/extensibility/change-primary-key-for-users-in-aspnet-identity
    public class ApplicationUserLogin : IdentityUserLogin<Guid> { }

    public class ApplicationUserRole : IdentityUserRole<Guid> { }

    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole>, IEntity
    {
        public ApplicationRole()
        {
            this.Id = Guid.NewGuid();
        }
    }

    public class ApplicationUserClaim : IdentityUserClaim<Guid> { }

    #endregion ASP.NET Identity utils
}