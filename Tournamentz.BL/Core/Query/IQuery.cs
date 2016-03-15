namespace Tournamentz.BL.Core.Query
{
    public interface IQuery
    {
        IExecutionContext UnitOfWork { get; set; }
    }
}