namespace Tournamentz.DAL.Entity.Core
{
    using System;

    public interface ITrackedEntity<TUser> : IEntity
        where TUser : IEntity
    {
        Guid CreatedById { get; set; }

        DateTime CreatedDate { get; set; }

        Guid? LastUpdatedById { get; set; }

        DateTime? LastUpdatedDate { get; set; }

        TUser CreatedBy { get; set; }
        TUser LastUpdatedBy { get; set; }
    }

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