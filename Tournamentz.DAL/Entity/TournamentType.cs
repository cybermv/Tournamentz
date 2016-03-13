namespace Tournamentz.DAL.Entity
{
    using Core;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TournamentTypes")]
    public class TournamentType : EntityBase
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}