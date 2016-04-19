namespace Tournamentz.DAL.Core
{
    using Entity.Core;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class GenericEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public GenericEntityRepository(IUnitOfWork owner)
        {
            this.Owner = owner;
        }

        public IUnitOfWork Owner { get; protected set; }

        public virtual IQueryable<TEntity> Query { get { return this.Owner.Context.Set<TEntity>(); } }

        IQueryable IRepository.Query { get { return this.Query; } }

        public virtual TEntity FindById(Guid id)
        {
            return this.Owner.Context.Set<TEntity>().Find(id);
        }

        object IRepository.FindById(Guid id)
        {
            return this.FindById(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            this.Owner.Context.Set<TEntity>().Add(entity);
            return this.Owner.Context.SaveChanges() > 0 ? entity : null;
        }

        public object Insert(object entity)
        {
            TEntity typedEntity = entity as TEntity;
            if (typedEntity == null)
            {
                throw new NotSupportedException(
                    string.Format("The current repository instance can only be used for entities of type '{0}'; given entity is of type '{1}'",
                        typeof(TEntity).Name,
                        entity.GetType().Name)
                    );
            }

            return this.Insert(typedEntity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            this.Owner.Context.Set<TEntity>().Attach(entity);
            this.Owner.Context.Entry(entity).State = EntityState.Modified;
            return this.Owner.Context.SaveChanges() > 0 ? entity : null;
        }

        public object Update(object entity)
        {
            TEntity typedEntity = entity as TEntity;
            if (typedEntity == null)
            {
                throw new NotSupportedException(
                    string.Format("The current repository instance can only be used for entities of type '{0}'; given entity is of type '{1}'",
                        typeof(TEntity).Name,
                        entity.GetType().Name)
                    );
            }

            return this.Update(typedEntity);
        }

        public virtual bool Delete(Guid id)
        {
            TEntity toDelete = FindById(id);

            if (toDelete != null)
            {
                this.Owner.Context.Set<TEntity>().Remove(toDelete);
                return this.Owner.Context.SaveChanges() > 0;
            }
            return false;
        }
    }
}