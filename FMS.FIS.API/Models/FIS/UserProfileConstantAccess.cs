namespace FMC.FIS.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserProfileConstantAccess")]
    public class UserProfileConstantAccess
    {

        [Key]
        [Column("IdUserProfileConstantAccess", TypeName = "int")]
        public int IdUserProfileConstantAccess { get; set; }

        [Column("IdUserProfile", TypeName = "int")]
        public int IdUserProfile { get; set; }

        [Column("IdConstantAccess", TypeName = "int")]
        public int IdConstantAccess { get; set; }

        [ForeignKey("IdConstantAccess")]
        public virtual ConstantAccess ConstantAccess { get; set; }

        [ForeignKey("IdUserProfile")]
        public virtual UserProfile UserProfile { get; set; }
    }
}
