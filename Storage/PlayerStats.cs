using System;
using System.ComponentModel.DataAnnotations;

namespace Storage
{
    public partial class PlayerStats
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int GamesPlayed { get; set; }

        public int GamesWon { get; set; }

        public int GamesLost { get; set; }

        [Required]
        [StringLength(50)]
        public string MostCommonRole { get; set; }

        public string MostKilled { get; set; }

        public string MostKilledBy { get; set; }

        public int MostCommonRolePercent { get; set; }

        public int GamesSurvived { get; set; }

        public DateTime? LastRun { get; set; }
    }
}
