using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.FIS
{
    [Table("Phone")]
    public class Phone
    {
        [Key]
        [Column("IdPhone", TypeName = "bigint")]
        public Int64 IdPhone { get; set; }

        [Column("IdPerson", TypeName = "bigint")]
        public Int64 IdPerson { get; set; }

        [Column("IdOrigem", TypeName = "tinyint")]
        public byte IdOrigem { get; set; }

        [Column("IdPhoneStatus", TypeName = "int")]
        public Int32 IdPhoneStatus { get; set; }

        [Column("IdPhoneType", TypeName = "int")]
        public Int32 IdPhoneType { get; set; }

        [MaxLength(11)]
        [Column("NrPhone", TypeName = "varchar")]
        public string NrPhone { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [Column("Blacklist", TypeName = "bit")]
        public bool Blacklist { get; set; }

        [ForeignKey("IdPerson")]
        public virtual Person Person { get; set; }

        [ForeignKey("IdPhoneType")]
        public virtual PhoneType PhoneType { get; set; }

        public virtual ICollection<TelegramCode> TelegramCode { get; set; }
    }
}
