namespace Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("db_owner.v_WaitList")]
    public partial class v_WaitList
    {
        [Key]
        [Column(Order = 0)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Expr1 { get; set; }
    }
}
