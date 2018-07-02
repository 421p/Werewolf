using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Storage
{
    [Table("Game")]
    public partial class Game
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game()
        {
            GameKill = new HashSet<GameKill>();
            GamePlayer = new HashSet<GamePlayer>();
        }

        public int Id { get; set; }

        [Required]
        public string GroupName { get; set; }

        public long GroupId { get; set; }

        public DateTime? TimeStarted { get; set; }

        public DateTime? TimeEnded { get; set; }

        [StringLength(50)]
        public string Winner { get; set; }

        public int? GrpId { get; set; }

        [StringLength(50)]
        public string Mode { get; set; }

        public virtual Group Group { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GameKill> GameKill { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GamePlayer> GamePlayer { get; set; }
    }
}
