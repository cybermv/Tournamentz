﻿namespace Tournamentz.BL.Core.Query
{
    using Interface;
    using System;
    using System.Linq;

    public abstract class BasicQueryBase<TQuery> :
        IBasicQuery,
        IQueryHandler<TQuery>,
        ICreateHandler<TQuery>
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