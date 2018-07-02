using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage
{
    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
    }
}
