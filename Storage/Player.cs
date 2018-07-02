using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Storage
{
    [Table("Player")]
    public partial class Player
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Player()
        {
            GameKill = new HashSet<GameKill>();
            GamePlayers = new HashSet<GamePlayer>();
            Group = new HashSet<Group>();
        }

        public int Id { get; set; }

        public int TelegramId { get; set; }

        [Required]
        public string Name { get; set; }

        public string UserName { get; set; }

        public bool? Banned { get; set; }

        public string BannedBy { get; set; }

        public bool? HasPM { get; set; }

        public string BanReason { get; set; }

        public string ImageFile { get; set; }

        public string Language { get; set; }

        public int? TempBanCount { get; set; }

        public bool? HasPM2 { get; set; }

        public bool? HasDebugPM { get; set; }

        public long? Achievements { get; set; }

        [StringLength(128)]
        public string WebUserId { get; set; }

        public int? DonationLevel { get; set; }

        public bool? Founder { get; set; }

        public string CustomGifSet { get; set; }

        public bool? GifPurchased { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GameKill> GameKill { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GamePlayer> GamePlayers { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group> Group { get; set; }
    }
}
