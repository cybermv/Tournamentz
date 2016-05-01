namespace Tournamentz.BL.Queries
{
    using System;
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
                        Title = t.Title
                    });
            }

            public string Title { get; set; }
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
    }
}
