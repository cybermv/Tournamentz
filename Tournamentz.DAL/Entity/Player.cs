namespace Tournamentz.DAL.Entity
{
    using Core;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Players")]
    public class Player : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string Nickname { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<TeamPlayer> TeamPlayers { get; set; }
    }
}