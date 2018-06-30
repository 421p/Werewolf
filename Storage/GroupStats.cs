namespace Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GroupStats
    {
        public int Id { get; set; }

        public long GroupId { get; set; }

        public int GamesPlayed { get; set; }

        public string MostKilledFirstNight { get; set; }

        public int? MostKilledFirstPercent { get; set; }

        public string MostLynchedFirstNight { get; set; }

        public int? MostLynchFirstPercent { get; set; }

        public string MostDeadFirstDay { get; set; }

        public int? MostDeadFirstPercent { get; set; }

        public string BestSurvivor { get; set; }

        public int? BestSurvivorPercent { get; set; }

        public DateTime? LastRun { get; set; }

        public string GroupName { get; set; }
    }
}
