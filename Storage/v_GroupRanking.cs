namespace Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_GroupRanking
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GroupId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TelegramId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(450)]
        public string Language { get; set; }

        public string Description { get; set; }

        public string GroupLink { get; set; }

        public decimal? Ranking { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime LastRefresh { get; set; }

        [Key]
        [Column(Order = 4)]
        public string Name { get; set; }
    }
}
