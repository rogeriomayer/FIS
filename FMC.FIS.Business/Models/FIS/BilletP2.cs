namespace FMC.FIS.Business.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BilletP2")]
    public class BilletP2      
    {
        [Key]
        [Column("IdBillet", TypeName = "bigint")]
        public long IdBillet { get; set; }

        [Column("CPF", TypeName = "varchar")]
        public string CPF { get; set; }

        [Column("VlBillet", TypeName = "decimal")]
        public decimal VlBillet { get; set; }

        [Column("Account", TypeName = "varchar")]
        public string Account { get; set; }

        [Column("CodeBar", TypeName = "varchar")]
        public string CodeBar { get; set; }

        [Column("Age", TypeName = "int")]
        public int Age { get; set; }

        [Column("Email", TypeName = "varchar")]
        public string Email { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

    }
}
