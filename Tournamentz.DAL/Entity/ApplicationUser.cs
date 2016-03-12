namespace Tournamentz.DAL.Entity
{
    using Core;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.ComponentModel.DataAnnotations;

    public sealed class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IUser<Guid>, IEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }

        [Required]
        [StringLength(50)]
        public string Ime { get; set; }

        [Required]
        [StringLength(50)]
        public string Prezime { get; set; }

        [StringLength(11, MinimumLength = 11)]
        [RegularExpression("^[0-9]*$")]
        public string Oib { get; set; }
    }

    #region ASP.NET Identity utils

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