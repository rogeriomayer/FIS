namespace FMC.FIS.Business.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserProfile")]
    public class UserProfile
    {
        public UserProfile()
        {
            UserProfileConstantAccess = new HashSet<UserProfileConstantAccess>();
        }

        [Key]
        [Column("IdUserProfile", TypeName = "int")]
        public int IdUserProfile { get; set; }

        [Column("DsProfile", TypeName = "varchar")]
        public string DsProfile { get; set; }

        [Column("IdProductType", TypeName = "tinyint")]
        public byte IdProductType { get; set; }

        [Column("LevelProfile", TypeName = "tinyint")]
        public byte LevelProfile { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        public virtual ICollection<UserProfileConstantAccess> UserProfileConstantAccess { get; set; }
    }
}
