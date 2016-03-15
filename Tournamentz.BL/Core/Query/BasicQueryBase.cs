namespace Tournamentz.BL.Core.Query
{
    using System;
    using System.Linq;

    public abstract class BasicQueryBase<TQuery> : IBasicQuery, IQueryHandler<TQuery>
        where TQuery : IBasicQuery, new()
    {
        public Guid Id { get; set; }

        public abstract IQueryable<TQuery> Query(IExecutionContext context);

        public virtual TQuery Create(IExecutionContext context)
        {
            return new TQuery { Id = Guid.NewGuid() };
        }
    }
}