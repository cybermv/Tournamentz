namespace Tournamentz.BL.Core.Query.Interface
{
    public interface IKeyValueQuery : IBasicQuery
    {
        string Text { get; set; }
    }
}