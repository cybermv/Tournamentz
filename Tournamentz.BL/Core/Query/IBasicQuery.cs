namespace Tournamentz.BL.Core.Query
{
    using System;

    public interface IBasicQuery : IQuery
    {
        Guid Id { get; set; }
    }
}