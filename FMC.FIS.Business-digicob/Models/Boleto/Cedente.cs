namespace FMC.FIS.Business.Models.Boleto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Cedente")]
    public class Cedente
    {
        public Cedente()
        {
            this.Boleto = new HashSet<Boleto>();
        }
        [Key, Column("IdCedente", TypeName = "bigint")]
        public long IdCedente { get; set; }

        [Column("CodigoCedente", TypeName = "varchar")]
        public string CodigoCedente { get; set; }

        [Column("CodigoCedenteFormatado", TypeName = "varchar")]
        public string CodigoCedenteFormatado { get; set; }

        [Column("CodigoTransmissao", TypeName = "varchar")]
        public string CodigoTransmissao { get; set; }

        [Column("Convenio", TypeName = "varchar")]
        public string Convenio { get; set; }

        [Column("CpfCnpj", TypeName = "varchar")]
        public string CpfCnpj { get; set; }

        [Column("DigitoCedente", TypeName = "varchar")]
        public string DigitoCedente { get; set; }

        [ForeignKey("IdEndereco")]
        public virtual Endereco Endereco { get; set; }

        [Column("Id", TypeName = "nchar")]
        public string Id { get; set; }

        [Column("IdContaBancariaCedente", TypeName = "bigint")]
        public long? IdContaBancariaCedente { get; set; }

        [Column("IdEndereco", TypeName = "bigint")]
        public long? IdEndereco { get; set; }

        [Column("Nome", TypeName = "varchar")]
        public string Nome { get; set; }

        public virtual ICollection<Boleto> Boleto { get; set; }

    }
}
