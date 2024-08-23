namespace FMC.Boletos.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    [Table("Boleto", Schema = "dbo")]
    public class Boleto
    {
        public Boleto()
        {
            //this.BoletoRemessa = new HashSet<BoletoRemessa>();
        }

        [Column("Aceite", TypeName = "varchar")]
        public string Aceite { get; set; }

        [Column("Carteira", TypeName = "varchar")]
        public string Carteira { get; set; }

        [Column("CodigoBarraBoleto", TypeName = "varchar")]
        public string CodigoBarraBoleto { get; set; }

        [Column("CodigoDoProduto", TypeName = "varchar")]
        public string CodigoDoProduto { get; set; }

        [Column("DataDesconto", TypeName = "datetime")]
        public DateTime? DataDesconto { get; set; }

        [Column("DataDocumento", TypeName = "datetime")]
        public DateTime DataDocumento { get; set; }

        [Column("DataEnvioBaixa", TypeName = "datetime")]
        public DateTime? DataEnvioBaixa { get; set; }

        [Column("DataEnvioRegistro", TypeName = "datetime")]
        public DateTime? DataEnvioRegistro { get; set; }

        [Column("DataJurosMora", TypeName = "datetime")]
        public DateTime? DataJurosMora { get; set; }

        [Column("DataMulta", TypeName = "datetime")]
        public DateTime? DataMulta { get; set; }

        [Column("DataOutrosAcrescimos", TypeName = "datetime")]
        public DateTime? DataOutrosAcrescimos { get; set; }

        [Column("DataOutrosDescontos", TypeName = "datetime")]
        public DateTime? DataOutrosDescontos { get; set; }

        [Column("DataProcessamento", TypeName = "datetime")]
        public DateTime DataProcessamento { get; set; }

        [Column("DataRegistro", TypeName = "datetime")]
        public DateTime? DataRegistro { get; set; }

        [Column("DataVencimento", TypeName = "datetime")]
        public DateTime DataVencimento { get; set; }

        [Column("DigitoNossoNumero", TypeName = "varchar")]
        public string DigitoNossoNumero { get; set; }

        [Column("DigitoNumeroDocumento", TypeName = "varchar")]
        public string DigitoNumeroDocumento { get; set; }

        [Column("FlBaixado", TypeName = "bit")]
        public bool? FlBaixado { get; set; }

        [Column("FlRegistrado", TypeName = "bit")]
        public bool FlRegistrado { get; set; }

        [Column("IdAcordo", TypeName = "bigint")]
        public long? IdAcordo { get; set; }

        [Column("IdBanco", TypeName = "bigint")]
        public long IdBanco { get; set; }

        [Key, Column("IdBoleto", TypeName = "bigint")]
        public long IdBoleto { get; set; }

        [Column("IdCarteiraCobranca", TypeName = "bigint")]
        public long IdCarteiraCobranca { get; set; }

        [Column("IdCedente", TypeName = "bigint")]
        public long IdCedente { get; set; }

        [Column("IdCodigoOcorrenciaRemessa", TypeName = "bigint")]
        public long? IdCodigoOcorrenciaRemessa { get; set; }

        [Column("IdCodigoOcorrenciaRetorno", TypeName = "bigint")]
        public long? IdCodigoOcorrenciaRetorno { get; set; }

        [Column("IdentificadorInternoBoleto", TypeName = "varchar")]
        public string IdentificadorInternoBoleto { get; set; }

        [Column("IdEspecieDocumento", TypeName = "int")]
        public int? IdEspecieDocumento { get; set; }

        [Column("IdLead", TypeName = "bigint")]
        public long IdLead { get; set; }

        [Column("IdParcela", TypeName = "bigint")]
        public long? IdParcela { get; set; }

        [Column("IdSacado", TypeName = "bigint")]
        public long IdSacado { get; set; }

        [Column("Iof", TypeName = "decimal")]
        public decimal? Iof { get; set; }

        [Column("JurosMora", TypeName = "decimal")]
        public decimal? JurosMora { get; set; }

        [Column("JurosPermanente", TypeName = "bit")]
        public bool JurosPermanente { get; set; }

        [Column("LinhaDigitavelBoleto", TypeName = "varchar")]
        public string LinhaDigitavelBoleto { get; set; }

        [Column("LocalPagamento", TypeName = "varchar")]
        public string LocalPagamento { get; set; }

        [Column("Moeda", TypeName = "varchar")]
        public string Moeda { get; set; }

        [Column("NossoNumero", TypeName = "varchar")]
        public string NossoNumero { get; set; }

        [Column("NumeroDocumento", TypeName = "varchar")]
        public string NumeroDocumento { get; set; }

        [Column("NumeroParcela", TypeName = "int")]
        public int NumeroParcela { get; set; }

        [Column("OutrosAcrescimos", TypeName = "decimal")]
        public decimal? OutrosAcrescimos { get; set; }

        [Column("OutrosDescontos", TypeName = "decimal")]
        public decimal? OutrosDescontos { get; set; }

        [Column("PercentualJurosMora", TypeName = "decimal")]
        public decimal? PercentualJurosMora { get; set; }

        [Column("PercentualMulta", TypeName = "decimal")]
        public decimal? PercentualMulta { get; set; }

        [Column("QtdDias", TypeName = "int")]
        public int? QtdDias { get; set; }

        [Column("QtdParcelas", TypeName = "int")]
        public int QtdParcelas { get; set; }

        [Column("QuantidadeMoeda", TypeName = "decimal")]
        public decimal? QuantidadeMoeda { get; set; }

        [Column("RowGuidBoleto", TypeName = "uniqueidentifier")]
        public Guid RowGuidBoleto { get; set; }

        [Column("TipoArquivo", TypeName = "int")]
        public int TipoArquivo { get; set; }

        [Column("TipoModalidade", TypeName = "varchar")]
        public string TipoModalidade { get; set; }

        [Column("ValorAbatimento", TypeName = "decimal")]
        public decimal? ValorAbatimento { get; set; }

        [Column("ValorBoleto", TypeName = "decimal")]
        public decimal ValorBoleto { get; set; }

        [Column("ValorCobrado", TypeName = "decimal")]
        public decimal? ValorCobrado { get; set; }

        [Column("ValorDesconto", TypeName = "decimal")]
        public decimal? ValorDesconto { get; set; }

        [Column("ValorDescontoDia", TypeName = "decimal")]
        public decimal? ValorDescontoDia { get; set; }

        [Column("ValorMoeda", TypeName = "decimal")]
        public decimal? ValorMoeda { get; set; }

        [Column("ValorMulta", TypeName = "decimal")]
        public decimal? ValorMulta { get; set; }

        [Column("PDF", TypeName = "varbinary")]
        public byte[] PDF { get; set; }
    }
}
