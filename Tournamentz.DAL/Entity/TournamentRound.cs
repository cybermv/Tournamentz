namespace Tournamentz.DAL.Entity
{
    using Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TournamentRounds")]
    public class TournamentRound : EntityBase
    {
        public Guid TournamentId { get; set; }

        public Guid GameId { get; set; }

        public virtual Tournament Tournament { get; set; }

        public virtual Game Game { get; set; }

        public virtual ICollection<TournamentRoundParticipant> TournamentRoundParticipants { get; set; }
    }
}