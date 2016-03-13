namespace Tournamentz.DAL.Entity
{
    using Core;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TeamsPlayers")]
    public class TeamPlayer : EntityBase
    {
        public Guid TeamId { get; set; }

        public Guid PlayerId { get; set; }

        public virtual Team Team { get; set; }

        public virtual Player Player { get; set; }
    }
}