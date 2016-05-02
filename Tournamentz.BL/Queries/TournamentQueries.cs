namespace Tournamentz.BL.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Core.Attribute;
    using Core.Query;
    using DAL.Entity;

    public abstract class TournamentQueries
    {
        [RequiresRole(TournamentzRoles.User)]
        public class My : BasicQueryBase<My>
        {
            public override IQueryable<My> Query(IExecutionContext context)
            {
                return context.UnitOfWork.Repository<Tournament>()
                    .Where(t => t.OrganizerId == context.User.Id)
                    .Select(t => new My
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Teams = t.Teams
                            .Select(tm => new TeamInfo
                            {
                                Id = tm.TeamId,
                                Title = tm.Team.Title
                            })
                            .ToList()
                    });
            }

            public string Title { get; set; }

            public List<TeamInfo> Teams { get; set; }

            public class TeamInfo
            {
                public Guid Id { get; set; }

                public string Title { get; set; }
            }
        }

        [RequiresRole(TournamentzRoles.Admin)]
        public class All : BasicQueryBase<All>
        {
            public override IQueryable<All> Query(IExecutionContext context)
            {
                return context.UnitOfWork.Repository<Tournament>()
                    .Select(t => new All
                    {
                        Id = t.Id,
                        Title = t.Title,
                        OrganizerId = t.Organizer.Player.Id,
                        OrganizerNickname = t.Organizer.Player.Nickname
                    });
            }

            public string Title { get; set; }

            public Guid OrganizerId { get; set; }

            public string OrganizerNickname { get; set; }
        }

        [RequiresRole(TournamentzRoles.User)]
        public class Types : KeyValueQueryBase<Types>
        {
            public override IQueryable<Types> Query(IExecutionContext context)
            {
                return context.UnitOfWork.Repository<TournamentType>()
                    .Select(tt => new Types
                    {
                        Id = tt.Id,
                        Text = tt.Name
                    });
            }
        }
    }
}
