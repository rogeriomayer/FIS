using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.CREDZ
{
    [Table("EmailRemember", Schema = "CREDZ")]
    public class EmailRemember
    {
        [Key]
        [Column("IdEmailRemember", TypeName = "bigint")]
        public long IdEmailRemember { get; set; }

        [Column("IdAgreement", TypeName = "bigint")]
        public long IdAgreement { get; set; }

        [Column("IdAgreementParcel", TypeName = "bigint")]
        public long IdAgreementParcel { get; set; }

        [Column("IdPerson", TypeName = "bigint")]
        public long IdPerson { get; set; }

        [Column("Email", TypeName = "varchar")]
        public string Email { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

    }
}
