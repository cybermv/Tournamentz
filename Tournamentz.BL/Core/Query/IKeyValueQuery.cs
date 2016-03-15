namespace Tournamentz.BL.Core.Query
{
    public interface IKeyValueQuery : IBasicQuery
    {
        string Text { get; set; }
    }
}