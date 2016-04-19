namespace Tournamentz.DAL.Core
{
    using Entity.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class GenericEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected DbSet<TEntity> DbSet;

        public GenericEntityRepository(IUnitOfWork owner)
        {
            this.Owner = owner;
            this.DbSet = owner.Context.Set<TEntity>();
        }

        public IUnitOfWork Owner { get; protected set; }

        public virtual IQueryable<TEntity> Query { get { return this.DbSet; } }

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

        #region IQueryable

        public IEnumerator<TEntity> GetEnumerator()
        {
            return ((IEnumerable<TEntity>)this.DbSet).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.DbSet).GetEnumerator();
        }

        public Expression Expression { get { return ((IQueryable<TEntity>)this.DbSet).Expression; } }

        public Type ElementType { get { return ((IQueryable<TEntity>)this.DbSet).ElementType; } }

        public IQueryProvider Provider { get { return ((IQueryable<TEntity>)this.DbSet).Provider; } }

        #endregion IQueryable
    }
}