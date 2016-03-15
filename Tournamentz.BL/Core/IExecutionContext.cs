namespace Tournamentz.BL.Core
{
    using Autofac;
    using DAL.Core;
    using DAL.Entity;
    using System;

    public interface IExecutionContext : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        ApplicationUser User { get; set; }

        IContainer Services { get; }
    }
}