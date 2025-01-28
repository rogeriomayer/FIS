using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.Boleto
{
    [Table("IdentificadorBin2")]
    public class IdentificadorBin
    {
        [Column("BinRange", TypeName = "varchar")]
        public string BinRange { get; set; }

        [Key, Column("Identificador", TypeName = "varchar")]
        public string Identificador { get; set; }

        [Column("ORG", TypeName = "varchar")]
        public string ORG { get; set; }

        [Column("LOGO", TypeName = "varchar")]
        public string LOGO { get; set; }

        [Column("Bandeira", TypeName = "varchar")]
        public string Bandeira { get; set; }

        [Column("Parceria", TypeName = "varchar")]
        public string Parceria { get; set; }

        [Column("Produto", TypeName = "varchar")]
        public string Produto { get; set; }

        [Column("DigBin", TypeName = "int")]
        public int DigBin { get; set; }
    }
}
