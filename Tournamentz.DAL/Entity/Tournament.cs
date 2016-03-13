namespace Tournamentz.DAL.Entity
{
    using Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tournaments")]
    public class Tournament : EntityBase
    {
        public Tournament()
        {
            this.StartDateTime = DateTime.Now;
        }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public Guid OrganizerId { get; set; }

        public Guid TournamentTypeId { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public virtual ApplicationUser Organizer { get; set; }

        public virtual TournamentType TournamentType { get; set; }

        public virtual ICollection<TournamentRound> Rounds { get; set; }
    }
}