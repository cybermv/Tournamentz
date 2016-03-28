namespace Tournamentz.BL.Core.Query
{
    using Interface;
    using System;
    using System.Linq;

    public abstract class ParameteredQueryBase<TQuery, TParam> :
        IBasicQuery,
        IParameteredQueryHandler<TQuery, TParam>,
        ICreateHandler<TQuery>
        where TQuery : IBasicQuery, new()
    {
        public Guid Id { get; set; }

        public abstract IQueryable<TQuery> Query(IExecutionContext context, TParam parameter);

        public virtual TQuery Create(IExecutionContext context)
        {
            return new TQuery { Id = Guid.NewGuid() };
        }
    }
}