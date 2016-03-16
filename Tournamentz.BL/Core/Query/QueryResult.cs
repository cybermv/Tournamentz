namespace Tournamentz.BL.Core.Query
{
    using System;
    using System.Linq;

    public class QueryResult<TQuery> : IQueryResult<TQuery>
        where TQuery : IQuery
    {
        private Exception _exception;

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

                case QueryResultStatus.SystemError:
                    return string.Format("System error; {0}", this.Exception);

                default:
                    return "Invalid result state";
            }
        }
    }
}