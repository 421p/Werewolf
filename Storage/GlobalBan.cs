namespace Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
