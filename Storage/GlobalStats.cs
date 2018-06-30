namespace Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GlobalStats
    {
        public int Id { get; set; }

        public int GamesPlayed { get; set; }

        public int PlayersKilled { get; set; }

        public int PlayersSurvived { get; set; }

        public string MostKilledFirstNight { get; set; }

        public int? MostKilledFirstPercent { get; set; }

        public string MostLynchedFirstDay { get; set; }

        public int? MostLynchedFirstPercent { get; set; }

        public string MostKilledFirstDay { get; set; }

        public int? MostKilledFirstDayPercent { get; set; }

        public string BestSurvivor { get; set; }

        public int? BestSurvivorPercent { get; set; }

        public DateTime? LastRun { get; set; }

        public int? TotalPlayers { get; set; }

        public int? TotalGroups { get; set; }

        public int? MostKilledFirstNightId { get; set; }

        public int? MostLynchedFirstDayId { get; set; }

        public int? MostKilledFirstDayId { get; set; }

        public int? BestSurvivorId { get; set; }
    }
}
