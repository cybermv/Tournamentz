namespace Tournamentz.BL.Core.Query
{
    using System;

    public abstract class KeyValueQueryBase<TQuery> : BasicQueryBase<TQuery>, IKeyValueQuery
        where TQuery : IKeyValueQuery, new()
    {
        public string Text { get; set; }

        public override TQuery Create(IExecutionContext context)
        {
            return new TQuery { Id = Guid.NewGuid(), Text = "" };
        }
    }
}