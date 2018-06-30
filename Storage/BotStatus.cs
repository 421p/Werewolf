namespace Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("db_owner.BotStatus")]
    public partial class BotStatus
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string BotName { get; set; }

        [Column("BotStatus")]
        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        public string BotLink { get; set; }
    }
}
