namespace Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GroupRanking")]
    public partial class GroupRanking
    {
        public int GroupId { get; set; }

        [Required]
        [StringLength(450)]
        public string Language { get; set; }

        public int PlayersCount { get; set; }

        public decimal MinutesPlayed { get; set; }

        public DateTime LastRefresh { get; set; }

        public decimal? Ranking { get; set; }

        public int Id { get; set; }

        public int GamesPlayed { get; set; }

        public bool? Show { get; set; }
    }
}
