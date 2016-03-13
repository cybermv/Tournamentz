namespace Tournamentz.DAL.Entity.Core
{
    using System;

    public abstract class EntityBase : IEntity
    {
        protected EntityBase()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}