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

        public class FilteredByName : ParameteredQueryBase<FilteredByName, FilteredByName.NameParam>
        {
            public class NameParam
            {
                public string Name { get; set; }
            }

            public override IQueryable<FilteredByName> Query(IExecutionContext context, NameParam parameter)
            {
                return context.UnitOfWork.Repository<Player>().Query
                    .Where(p => p.Name.Contains(parameter.Name) ||
                                p.Surname.Contains(parameter.Name))
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