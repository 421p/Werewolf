namespace Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GamePlayer")]
    public partial class GamePlayer
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int GameId { get; set; }

        public bool Survived { get; set; }

        public bool Won { get; set; }

        [StringLength(50)]
        public string Role { get; set; }

        public virtual Game Game { get; set; }

        public virtual Player Player { get; set; }
    }
}
