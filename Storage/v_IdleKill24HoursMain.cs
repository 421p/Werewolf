using System.ComponentModel.DataAnnotations;

namespace Storage
{
    public partial class v_IdleKill24HoursMain
    {
        public int? Idles { get; set; }

        [Key]
        public string Name { get; set; }

        public string UserName { get; set; }
    }
}
