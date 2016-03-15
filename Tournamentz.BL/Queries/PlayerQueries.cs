namespace Tournamentz.BL.Queries
{
    using Core;
    using Core.Query;
    using DAL.Entity;
    using System.Linq;

    public abstract class PlayerQueries
    {
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
    }
}