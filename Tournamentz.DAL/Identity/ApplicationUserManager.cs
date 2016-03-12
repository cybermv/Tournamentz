namespace Tournamentz.DAL.Identity
{
    using Core;
    using Entity;
    using Microsoft.AspNet.Identity;
    using System;

    public class ApplicationUserManager : UserManager<ApplicationUser, Guid>
    {
        public ApplicationUserManager(IUnitOfWork uow)
            : base(new ApplicationUserStore(uow.Context))
        {
        }
    }
}