namespace Tournamentz.DAL.Core
{
    using Entity.Core;
    using System;
    using System.Linq;

    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IUnitOfWork Owner { get; }

        IQueryable<TEntity> Query { get; }

        TEntity FindById(Guid id);

        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);

        bool Delete(Guid id);
    }
}