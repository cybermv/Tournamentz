namespace Tournamentz.BL.Core.Query
{
    using Rule;
    using System;
    using System.Linq;

    public class QueryResult<TQuery> : IQueryResult<TQuery>
        where TQuery : IQuery
    {
        private Exception _exception;

        public QueryResult()
        {
            this.PermissionRules = new BusinessRuleCollection();
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

        public IQueryable<TQuery> Query { get; set; }

        public override string ToString()
        {
            switch (this.Status)
            {
                case QueryResultStatus.Success:
                    return string.Format("Success; query = {0}", this.Query);

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