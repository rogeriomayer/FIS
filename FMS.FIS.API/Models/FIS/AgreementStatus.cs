using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.API.Models.FIS
{
    [Table("AgreementStatus")]
    public class AgreementStatus
    {
        [Key]

        [Column("IdAgreementStatus", TypeName = "int")]
        public int IdAgreementStatus { get; set; }

        [Column("DsAgreementStatus", TypeName = "varchar")]
        public string DsAgreementStatus { get; set; }
    }
}
