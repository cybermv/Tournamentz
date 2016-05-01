namespace Tournamentz.DAL.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Core;

    [Table("TournamentsTeams")]
    public class TournamentTeam : EntityBase
    {
        public Guid TournamentId { get; set; }

        public Guid TeamId { get; set; }

        public virtual Tournament Tournament { get; set; }

        public virtual Team Team { get; set; }
    }
}
