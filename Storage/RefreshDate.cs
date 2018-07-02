using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage
{
    [Table("RefreshDate")]
    public partial class RefreshDate
    {
        [Key]
        [StringLength(1)]
        public string Lock { get; set; }

        public DateTime Date { get; set; }
    }
}
