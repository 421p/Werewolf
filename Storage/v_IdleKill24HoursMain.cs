namespace Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_IdleKill24HoursMain
    {
        public int? Idles { get; set; }

        [Key]
        public string Name { get; set; }

        public string UserName { get; set; }
    }
}
