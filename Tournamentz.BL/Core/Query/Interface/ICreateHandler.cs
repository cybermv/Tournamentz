namespace Tournamentz.BL.Core.Query.Interface
{
    public interface ICreateHandler<TQuery>
        where TQuery : IQuery
    {
        TQuery Create(IExecutionContext context);
    }
}