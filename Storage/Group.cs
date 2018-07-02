using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Storage
{
    [Table("Group")]
    public partial class Group
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Group()
        {
            Game = new HashSet<Game>();
            Player = new HashSet<Player>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public long GroupId { get; set; }

        public bool? Preferred { get; set; }

        public string Language { get; set; }

        public bool? DisableNotification { get; set; }

        public string UserName { get; set; }

        public bool? BotInGroup { get; set; }

        public bool? ShowRoles { get; set; }

        [StringLength(50)]
        public string Mode { get; set; }

        public int? DayTime { get; set; }

        public int? NightTime { get; set; }

        public int? LynchTime { get; set; }

        public bool? AllowTanner { get; set; }

        public bool? AllowFool { get; set; }

        public bool? AllowCult { get; set; }

        [StringLength(50)]
        public string ShowRolesEnd { get; set; }

        public int? MaxPlayers { get; set; }

        public bool? DisableFlee { get; set; }

        public string CreatedBy { get; set; }

        public string ImageFile { get; set; }

        public string Description { get; set; }

        public string GroupLink { get; set; }

        public int? MemberCount { get; set; }

        public bool? AllowExtend { get; set; }

        public int? MaxExtend { get; set; }

        public bool? EnableSecretLynch { get; set; }

        public bool? RandomModes { get; set; }

        public long? Flags { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game> Game { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Player> Player { get; set; }
        
        public void UpdateFlags()
        {
            if (Flags == null)
            {
                Flags = 0;
            }

            if (!HasFlag(GroupConfig.Update)) //special flag indicating group needs to be updated.
                return;
            RemoveFlag(GroupConfig.Update);
            foreach (var flag in Enum.GetValues(typeof(GroupConfig)).Cast<GroupConfig>().Where(x => x.IsEditable()))
            {
                try
                {
                    if ((bool?) GetType().GetProperty(flag.ToString()).GetValue(this) == true)
                        AddFlag(flag);
                    //check if the setting wasn't set before.
                    if (GetType().GetProperty(flag.ToString()).GetValue(this) == null)
                    {
                        //check the default
                        if (flag.GetDefaultValue())
                            AddFlag(flag);
                    }

                    //GetType().GetProperty(flag.ToString()).SetValue(this, null);
                }
                catch (NullReferenceException)
                {
                    //property doesn't exist, ignore
                }
            }


            //RemoveFlag(GroupConfig.Update);
        }

        public bool HasFlag(GroupConfig flag)
        {
            if (Flags == null) Flags = 0;
            var f = (GroupConfig) Flags;
            return f.HasFlag(flag);
        }

        public void AddFlag(GroupConfig flag)
        {
            if (Flags == null) Flags = 0;
            var f = (GroupConfig) Flags;
            f = f | flag;
            Flags = (long) f;
        }

        public void RemoveFlag(GroupConfig flag)
        {
            if (Flags == null) Flags = 0;
            var f = (GroupConfig) Flags;
            f &= ~flag;
            Flags = (long) f;
        }
    }
}
