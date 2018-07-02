using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage
{
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
