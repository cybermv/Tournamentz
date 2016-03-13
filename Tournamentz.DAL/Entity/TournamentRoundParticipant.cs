namespace Tournamentz.DAL.Entity
{
    using Core;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TournamentRoundsParticipants")]
    public class TournamentRoundParticipant : EntityBase
    {
        public Guid TournamentRoundId { get; set; }

        public Guid TeamId { get; set; }

        public decimal Score { get; set; }

        public virtual TournamentRound TournamentRound { get; set; }

        public virtual Team Team { get; set; }
    }
}