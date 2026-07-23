using Swashbuckle.AspNetCore.Annotations;
using System;

namespace FMC.FIS.Business.Models.Customer
{
    /// <summary>
    /// Dados do boleto
    /// </summary>
    [SwaggerSchema(Description = "Dados do boleto")]
    public class BilletResponse
    {
        /// <summary>
        /// Id do boleto
        /// </summary>
        [SwaggerSchema(Description = "Id do boleto")]
        public long IdBillet { get; set; }

        /// <summary>
        /// Id do produto
        /// </summary>
        [SwaggerSchema(Description = "Id do produto")]
        public long IdProduct { get; set; }

        /// <summary>
        /// Id da parcela do acordo
        /// </summary>
        [SwaggerSchema(Description = "Id da parcela do acordo")]
        public Nullable<long> IdAgreementParcel { get; set; }

        /// <summary>
        /// Id da promessa
        /// </summary>
        [SwaggerSchema(Description = "Id da promessa")]
        public Nullable<long> IdPromisse { get; set; }

        /// <summary>
        /// Valor do boleto
        /// </summary>
        [SwaggerSchema(Description = "Valor do boleto")]
        public decimal VlBillet { get; set; }


        /// <summary>
        /// Data de vencimento do boleto
        /// </summary>
        [SwaggerSchema(Description = "Data de vencimento do boleto")]
        public DateTime DtBillet { get; set; }

        /// <summary>
        /// Código de barras do boleto
        /// </summary>
        [SwaggerSchema(Description = "Código de barras do boleto")]
        public string Barcode { get; set; }

        /// <summary>
        /// Linha digitavel do boleto
        /// </summary>
        [SwaggerSchema(Description = "Linha digitavel do boleto")]
        public string Line { get; set; }

        /// <summary>
        /// Nosso numero do boleto
        /// </summary>
        [SwaggerSchema(Description = "Nosso numero do boleto")]
        public string DocumentNumber { get; set; }


        /// <summary>
        /// Data de criação do boleto
        /// </summary>
        [SwaggerSchema(Description = "Data de criação do boleto")]
        public DateTime DtInsert { get; set; }

        /// <summary>
        /// Quantidade de envios por e-mail
        /// </summary>
        [SwaggerSchema(Description = "Quantidade de envios por e-mail")]
        public int NrSendEmail { get; set; }

        /// <summary>
        /// Quantidade de envios por sms
        /// </summary>
        [SwaggerSchema(Description = "Quantidade de envios por sms")]
        public int NrSendSMS { get; set; }

        /// <summary>
        /// Código do acordo no sistema do cliente
        /// </summary>
        [SwaggerSchema(Description = "Código do acordo no sistema do cliente")]
        public string CdAgreement { get; set; }

        /// <summary>
        /// Código do boleto no sistema do cliente
        /// </summary>
        [SwaggerSchema(Description = "Código do boleto no sistema do cliente")]
        public string CdBillet { get; set; }

        /// <summary>
        /// URL do PDF do boleto
        /// </summary>
        [SwaggerSchema(Description = "URL do PDF do boleto")]
        public string URL { get; set; }

        /// <summary>
        /// Parcela do acordo
        /// </summary>
        [SwaggerSchema(Description = "Parcela do acordo")]
        public int Parcel { get; set; }
    }


    /// <summary>
    /// Dados entrada novo boleto
    /// </summary>
    [SwaggerSchema(Description = "Dados entrada novo boleto")]
    public class NewBilletRequest
    {

        /// <summary>
        /// Id do produto (conta/contrato)
        /// </summary>
        [SwaggerSchema(Description = "Id do produto (conta/contrato)")]
        public long IdProduct { get; set; }

        /// <summary>
        /// Id do acordo
        /// </summary>
        [SwaggerSchema(Description = "Id do acordo")]
        public long IdAgreement { get; set; }

        /// <summary>
        /// Id da parcela do acordo
        /// </summary>
        [SwaggerSchema(Description = "Id da parcela do acordo")]
        public long IdAgreementParcel { get; set; }

        /// <summary>
        /// Numero da parcela do acordo
        /// </summary>
        [SwaggerSchema(Description = "Numero da parcela do acordo")]
        public int NrParcel { get; set; }

        /// <summary>
        /// Id da promessa
        /// </summary>
        [SwaggerSchema(Description = "Id da promessa")]
        public long IdPromisse { get; set; }

        /// <summary>
        /// Codigo do acordo no sistema do cliente/parceiro
        /// </summary>
        [SwaggerSchema(Description = "Codigo do acordo no sistema do cliente/parceiro")]
        public string CdAgreement { get; set; }

        /// <summary>
        /// Valor do boleto
        /// </summary>
        [SwaggerSchema(Description = "Valor do boleto")]
        public decimal VlBillet { get; set; }

        /// <summary>
        /// Data de vencimento do boleto
        /// </summary>
        [SwaggerSchema(Description = "Data de vencimento do boleto")]
        public DateTime DtBillet { get; set; }

        /// <summary>
        /// CPF do cliente
        /// </summary>
        [SwaggerSchema(Description = "CPF do cliente")]
        public string CPF { get; set; }

    }
}
