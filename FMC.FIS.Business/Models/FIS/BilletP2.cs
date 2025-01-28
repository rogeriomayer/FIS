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

    public class BilletParameterP2
    {
        public long IdBoleto { get; set; }

        public long IdLead { get; set; }

        public string CodigoAcordo { get; set; }

        public string Parcela { get; set; }

        public string TotaldeParcelas { get; set; }

        public string Cedente { get; set; }

        public string Agencia { get; set; }

        public string ContaCorrente { get; set; }

        public string NomeSacado { get; set; }

        public string CPFSacado { get; set; }

        public string CNPJBeneficiario { get; set; }

        public string NomeBeneficiario { get; set; }

        public string AgenciaBeneficiario { get; set; }

        public string ContaBeneficiario { get; set; }

        public string ContaBeneficiarioDV { get; set; }

        public string EnderecoSacado { get; set; }

        public string NumeroSacado { get; set; }

        public string ComplementoSacado { get; set; }

        public string BairroSacado { get; set; }

        public string CidadeSacado { get; set; }

        public string EstadoSacado { get; set; }

        public string CepSacado { get; set; }

        public DateTime DataVencimento { get; set; }

        public string NumeroDocumento { get; set; }

        public Decimal ValorDocumento { get; set; }

        public string ContaDV { get; set; }

        public string Carteira { get; set; }

        public string Instrucao1 { get; set; }

        public string Instrucao2 { get; set; }

        public string Email { get; set; }

        public string ComplementoInstrucao { get; set; }

        public string Logo { get; set; }

        public string CodEmpresa { get; set; }
    }
}
