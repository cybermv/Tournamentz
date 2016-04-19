namespace Tournamentz.DAL.Core
{
    using Entity.Core;
    using System;
    using System.Linq;

    public interface IRepository : IQueryable
    {
        IUnitOfWork Owner { get; }

        IQueryable Query { get; }

        object FindById(Guid id);

        object Insert(object entity);

        object Update(object entity);

        bool Delete(Guid id);

    }

    public interface IRepository<TEntity> : IRepository, IQueryable<TEntity>
        where TEntity : class, IEntity
    {
        new IQueryable<TEntity> Query { get; }

        new TEntity FindById(Guid id);

        new TEntity Insert(TEntity entity);

        new TEntity Update(TEntity entity);

        new bool Delete(Guid id);
    }
}