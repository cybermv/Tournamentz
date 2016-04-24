namespace Tournamentz.BL.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Core.Query;
    using DAL.Core;
    using DAL.Entity;

    public abstract class TeamQueries
    {
        public class My : BasicQueryBase<My>
        {
            public My()
            {
                this.Players=new EnumerableQuery<PlayerInfo>(new List<PlayerInfo>());
            }

            public override IQueryable<My> Query(IExecutionContext context)
            {
                IRepository<Team> teamRepo = context.UnitOfWork.Repository<Team>();

                return teamRepo.Query
                    .Where(t => t.CreatorId == context.User.Id)
                    .Select(t => new My
                    {
                        Id = t.Id,
                        Title = t.Title,
                        /*Players = t.TeamPlayers
                            .AsQueryable() // TODO mvelenik: ovo ne valja jebemti sunce
                            .Select(tp => new PlayerInfo
                            {
                                Id = tp.PlayerId,
                                Nickname = tp.Player.Nickname
                            })*/
                    });
                ;
            }

            public string Title { get; set; }

            public IQueryable<PlayerInfo> Players { get; set; }

            public class PlayerInfo
            {
                public Guid  Id { get; set; }

                public string Nickname { get; set; }
            }
        }

        public class PlayingIn : BasicQueryBase<PlayingIn>
        {
            public override IQueryable<PlayingIn> Query(IExecutionContext context)
            {
                IRepository<Team> teamRepo = context.UnitOfWork.Repository<Team>();

                return teamRepo.Query
                    .Where(t => t.TeamPlayers
                        .Any(p => p.PlayerId == context.User.Player.Id))
                    .Select(t => new PlayingIn
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Creator = t.Creator.Player.Nickname
                    });
            }

            public string Title { get; set; }

            public string Creator { get; set; }
        }

        public class All : BasicQueryBase<All>
        {
            public override IQueryable<All> Query(IExecutionContext context)
            {
                IRepository<Team> teamRepo = context.UnitOfWork.Repository<Team>();

                return teamRepo.Query
                    .Select(t => new All
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Creator = t.Creator.Player.Nickname
                    });
            }

            public string Title { get; set; }

            public string Creator { get; set; }
        }
    }
}
