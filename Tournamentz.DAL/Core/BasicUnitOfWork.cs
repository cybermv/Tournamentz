namespace Tournamentz.DAL.Core
{
    using Entity.Core;
    using System;
    using System.Data;
    using System.Data.Entity;

    public class BasicUnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContextTransaction _transaction;
        private readonly object _lock = new object();
        private bool _isDisposed;

        public BasicUnitOfWork(DbContext context)
            : this(context, IsolationLevel.ReadCommitted)
        {
        }

        public BasicUnitOfWork(DbContext context, IsolationLevel isolationLevel)
        {
            this.Context = context;
            this.StartTransaction(isolationLevel);
            this._isDisposed = false;
        }

        public DbContext Context { get; protected set; }

        public virtual IRepository<TEntity> Repository<TEntity>()
            where TEntity : class, IEntity
        {
            lock (this._lock)
            {
                return new GenericEntityRepository<TEntity>(this);
            }
        }

        public virtual void Commit()
        {
            lock (this._lock)
            {
                this.ThrowIfDisposed();

                if (this._transaction == null)
                {
                    throw new InvalidOperationException("The transaction is not started or already committed/disposed");
                }

                this._transaction.Commit();
                this.KillTransaction();
            }
        }

        public virtual void Rollback()
        {
            lock (this._lock)
            {
                this.ThrowIfDisposed();

                if (this._transaction == null)
                {
                    throw new InvalidOperationException("The transaction is not started or already committed/disposed");
                }

                this._transaction.Rollback();
                this.KillTransaction();
            }
        }

        public void Dispose()
        {
            lock (this._lock)
            {
                this.ThrowIfDisposed();

                if (this._transaction != null)
                {
                    this.Rollback();
                    KillTransaction();
                }

                this.Context.Dispose();
                this._isDisposed = true;
            }
        }

        private void ThrowIfDisposed()
        {
            if (this._isDisposed)
            {
                throw new ObjectDisposedException("The current UnitOfWork is disposed");
            }
        }

        private void StartTransaction(IsolationLevel isolationLevel)
        {
            if (this._transaction != null)
            {
                throw new InvalidOperationException("The transaction is already started");
            }

            this._transaction = this.Context.Database.BeginTransaction(isolationLevel);
        }

        private void KillTransaction()
        {
            if (this._transaction == null)
            {
                throw new InvalidOperationException("The transaction is not started");
            }

            this._transaction.Dispose();
            this._transaction = null;
        }
    }
}