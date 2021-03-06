﻿namespace Tournamentz.BL.Core.Query
{
    using Interface;
    using Rule;
    using System;
    using System.Linq;

    public class QueryResult<TQuery> : IQueryResult<TQuery>
        where TQuery : IQuery
    {
        private Exception _exception;
        private IQueryable<TQuery> _innerQueryable;
        private string _sqlQuery;

        public QueryResult()
        {
            this.PermissionRules = new BusinessRuleCollection();
            this.Query = Enumerable.Empty<TQuery>().AsQueryable();
        }

        public BusinessRuleCollection PermissionRules { get; set; }

        public Exception Exception
        {
            get { return this._exception; }
            set
            {
                if (this._exception != null)
                {
                    throw new InvalidOperationException("Cannot set the Exception property more than once");
                }
                this._exception = value;
            }
        }

        public QueryResultStatus Status
        {
            get
            {
                if (this.Exception != null) { return QueryResultStatus.SystemError; }
                if (this.PermissionRules.Any(r => r.IsBroken)) { return QueryResultStatus.PermissionError; }
                return QueryResultStatus.Success;
            }
        }

        IQueryable IQueryResult.Query { get { return this.Query; } }

        public IQueryable<TQuery> Query
        {
            get
            {
                return this._innerQueryable;
            }
            set { this._innerQueryable = value;
                this._sqlQuery = value.ToString();
            }
        }

        public override string ToString()
        {
            switch (this.Status)
            {
                case QueryResultStatus.Success:
                    // TODO: the access
                    return string.Format("Success; query = {0}", this._sqlQuery);

                case QueryResultStatus.PermissionError:
                    return string.Format("Permission error; count = {0}", this.PermissionRules.Count(p => p.IsBroken));

                case QueryResultStatus.SystemError:
                    return string.Format("System error; {0}", this.Exception);

                default:
                    return "Invalid result state";
            }
        }
    }
}