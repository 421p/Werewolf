using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage
{
    [Table("db_owner.GlobalBan")]
    public partial class GlobalBan
    {
        public int Id { get; set; }

        public int TelegramId { get; set; }

        [Required]
        public string Reason { get; set; }

        public DateTime Expires { get; set; }

        [Required]
        public string BannedBy { get; set; }

        public DateTime? BanDate { get; set; }

        public string Name { get; set; }
    }
}
