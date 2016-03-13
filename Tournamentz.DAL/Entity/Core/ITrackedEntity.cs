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
}