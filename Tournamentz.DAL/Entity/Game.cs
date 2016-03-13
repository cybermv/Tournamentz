namespace Tournamentz.DAL.Entity
{
    using Core;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Games")]
    public class Game : EntityBase
    {
        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public Guid GameTypeId { get; set; }

        public virtual GameType GameType { get; set; }
    }
}