namespace Tournamentz.BL.Core.Query.Interface
{
    using System;

    public interface IBasicQuery : IQuery
    {
        Guid Id { get; set; }
    }
}