namespace Tournamentz.DAL.Entity
{
    using Core;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("GameTypes")]
    public class GameType : EntityBase
    {
        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}