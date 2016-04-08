namespace Tournamentz.BL.Queries
{
    using Core;
    using Core.Attribute;
    using Core.Query;
    using DAL.Entity;
    using System.Linq;

    public abstract class PlayerQueries
    {
        [RequiresRole(TournamentzRoles.Admin)]
        public class All : BasicQueryBase<All>
        {
            public override IQueryable<All> Query(IExecutionContext context)
            {
                return context.UnitOfWork.Repository<Player>()
                    .Query
                    .Select(p => new All
                    {
                        Id = p.Id,
                        Nickname = p.Nickname,
                        NameSurname = p.Name + " " + p.Surname
                    });
            }

            public string Nickname { get; set; }

            public string NameSurname { get; set; }
        }

        [RequiresRole(TournamentzRoles.Admin)]
        public class Dropdown : KeyValueQueryBase<Dropdown>
        {
            public override IQueryable<Dropdown> Query(IExecutionContext context)
            {
                return context.UnitOfWork.Repository<Player>()
                    .Query
                    .Select(p => new Dropdown
                    {
                        Id = p.Id,
                        Text = p.Nickname + " - " + p.Name + " " + p.Surname
                    });
            }
        }

        [RequiresRole(TournamentzRoles.Admin)]
        public class FilteredByName : ParameteredQueryBase<FilteredByName, string>
        {
            public override IQueryable<FilteredByName> Query(IExecutionContext context, string nameFilter)
            {
                return context.UnitOfWork.Repository<Player>().Query
                    .Where(p => p.Name.Contains(nameFilter) ||
                                p.Surname.Contains(nameFilter))
                    .Select(p => new FilteredByName
                    {
                        Id = p.Id,
                        FriendlyName = p.Name + " " + p.Surname
                    });
            }

            public string FriendlyName { get; set; }
        }
    }
}