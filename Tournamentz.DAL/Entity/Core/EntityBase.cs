namespace Tournamentz.DAL.Entity.Core
{
    using System;

    public interface IEntity
    {
        Guid Id { get; set; }
    }

    public abstract class EntityBase : IEntity
    {
        protected EntityBase()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}