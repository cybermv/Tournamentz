namespace Tournamentz.DAL.Entity
{
    using Core;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Teams")]
    public class Team : EntityBase
    {
        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public virtual ICollection<TeamPlayer> TeamPlayers { get; set; }

        public virtual ICollection<TournamentRoundParticipant> TournamentRoundParticipants { get; set; }
    }
}