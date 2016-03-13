namespace Tournamentz.DAL.Core
{
    using Entity.Core;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserUnitOfWork<TUser> : BasicUnitOfWork
        where TUser : class, IEntity
    {
        public UserUnitOfWork(DbContext context, TUser user)
            : base(context)
        {
            this.User = user;
        }

        public TUser User { get; protected set; }

        public override IRepository<TEntity> Repository<TEntity>()
        {
            if (typeof(TEntity).GetInterfaces().Contains(typeof(ITrackedEntity<TUser>)))
            {
                Type repoType = typeof(TrackedEntityRepository<,>).MakeGenericType(typeof(TEntity), typeof(TUser));
                object repoInstance = Activator.CreateInstance(repoType, this, this.User);
                return (IRepository<TEntity>)repoInstance;
            }

            return base.Repository<TEntity>();
        }
    }
}