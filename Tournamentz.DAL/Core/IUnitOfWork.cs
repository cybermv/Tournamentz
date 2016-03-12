namespace Tournamentz.DAL.Core
{
    using Entity.Core;
    using System;
    using System.Data.Entity;

    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }

        IRepository<TEntity> Repository<TEntity>()
            where TEntity : class, IEntity;

        void Commit();

        void Rollback();
    }
}