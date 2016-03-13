namespace Tournamentz.DAL.Entity.Core
{
    using System;

    public abstract class TrackedEntityBase<TUser> : EntityBase, ITrackedEntity<TUser>
        where TUser : IEntity
    {
        protected TrackedEntityBase()
            : base()
        {
            this.CreatedDate = DateTime.Now;
        }

        public Guid CreatedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid? LastUpdatedById { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public virtual TUser CreatedBy { get; set; }

        public virtual TUser LastUpdatedBy { get; set; }
    }
}