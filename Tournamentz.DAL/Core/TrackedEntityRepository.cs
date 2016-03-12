namespace Tournamentz.DAL.Core
{
    using Entity.Core;
    using System;

    public class TrackedEntityRepository<TEntity, TUser> : GenericEntityRepository<TEntity>
        where TEntity : class, ITrackedEntity<TUser>
        where TUser : class, IEntity
    {
        public TrackedEntityRepository(IUnitOfWork owner, TUser user)
            : base(owner)
        {
            this.User = user;
        }

        public TUser User { get; protected set; }

        public override TEntity Insert(TEntity entity)
        {
            entity.CreatedById = this.User.Id;
            entity.CreatedDate = DateTime.Now;
            return base.Insert(entity);
        }

        public override TEntity Update(TEntity entity)
        {
            entity.LastUpdatedById = this.User.Id;
            entity.LastUpdatedDate = DateTime.Now;
            return base.Update(entity);
        }
    }
}