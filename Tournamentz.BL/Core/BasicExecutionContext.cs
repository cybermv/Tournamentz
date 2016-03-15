namespace Tournamentz.BL.Core
{
    using Autofac;
    using DAL.Core;
    using DAL.Entity;

    public class BasicExecutionContext : IExecutionContext
    {
        public BasicExecutionContext(IUnitOfWork unitOfWork, IContainer container)
        {
            this.UnitOfWork = unitOfWork;
            this.Services = container;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public ApplicationUser User { get; set; }

        public IContainer Services { get; private set; }

        public void Dispose()
        {
            this.UnitOfWork.Dispose();
        }
    }
}