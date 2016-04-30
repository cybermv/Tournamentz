namespace Tournamentz.BL.Extensions
{
    using System;
    using System.Linq;
    using DAL.Core;
    using DAL.Entity;

    public static class PlayerRepositoryExtensions
    {
        public static Player FindByNickname(this IRepository<Player> repo, string nickname)
        {
            return repo
                .SingleOrDefault(p => p.Nickname == nickname);
        }
    }
}
