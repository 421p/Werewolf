namespace Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GameKill")]
    public partial class GameKill
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int KillerId { get; set; }

        public int VictimId { get; set; }

        public DateTime TimeStamp { get; set; }

        public int KillMethodId { get; set; }

        public int Day { get; set; }

        public virtual Game Game { get; set; }

        public virtual Player Player { get; set; }

        public virtual KillMethod KillMethod { get; set; }
    }
}
