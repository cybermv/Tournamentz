namespace Tournamentz.DAL.Core
{
    using Entity.Core;
    using System;
    using System.Data;
    using System.Data.Entity;

    public class BasicUnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContextTransaction _transaction;

        public BasicUnitOfWork(DbContext context)
        {
            this.Context = context;
            StartTransaction();
        }

        public DbContext Context { get; protected set; }

        public virtual IRepository<TEntity> Repository<TEntity>()
            where TEntity : class, IEntity
        {
            return new GenericEntityRepository<TEntity>(this);
        }

        public virtual void Commit()
        {
            if (this._transaction != null &&
                this._transaction.UnderlyingTransaction.Connection != null &&
                this._transaction.UnderlyingTransaction.Connection.State != ConnectionState.Closed)
            {
                this._transaction.Commit();
                KillTransaction();
            }
        }

        public virtual void Rollback()
        {
            if (this._transaction != null &&
                this._transaction.UnderlyingTransaction.Connection != null &&
                this._transaction.UnderlyingTransaction.Connection.State != ConnectionState.Closed)
            {
                this._transaction.Rollback();
                KillTransaction();
            }
        }

        public void Dispose()
        {
            if (this._transaction != null)
            {
                Rollback();
                KillTransaction();
            }
            this.Context.Dispose();
        }

        private void StartTransaction()
        {
            if (_transaction == null)
            {
                this._transaction = this.Context.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            }
        }

        private void KillTransaction()
        {
            if (this._transaction != null)
            {
                this._transaction.Dispose();
                this._transaction = null;
            }
        }
    }
}