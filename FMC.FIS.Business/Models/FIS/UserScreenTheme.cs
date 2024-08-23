namespace FMC.FIS.Business.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserProfileScreen")]
    public class UserScreenTheme
    {
        [Key]
        [Column("IdUserScreenTheme", TypeName = "int")]
        public int IdUserScreenTheme { get; set; }

        [Column("IdUserLogin", TypeName = "int")]
        public int IdUserLogin { get; set; }

        [Column("Theme", TypeName = "varchar")]
        public string Theme { get; set; }

        [Column("FlOccurrence", TypeName = "bit")]
        public bool FlOccurrence { get; set; }

        [Column("FlOccurrenceDetail", TypeName = "bit")]
        public bool FlOccurrenceDetail { get; set; }

        [Column("FlEstatementDetail", TypeName = "bit")]
        public bool FlEstatementDetail { get; set; }

        [Column("DtUpdate", TypeName = "datetime")]
        public DateTime DtUpdate { get; set; }

        [ForeignKey("IdUser")]
        public virtual UserLogin UserLogin { get; set; }
    }
}
