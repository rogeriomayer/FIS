namespace FMC.FIS.API.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BilletP1")]
    public class BilletP1      
    {
        [Key]
        [Column("IdBilletP1", TypeName = "bigint")]
        public long IdBilletP1 { get; set; }

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
