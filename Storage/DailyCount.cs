namespace Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DailyCount")]
    public partial class DailyCount
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Day { get; set; }

        public int Groups { get; set; }

        public int Games { get; set; }

        public int Users { get; set; }
    }
}
