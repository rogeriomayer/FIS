using Swashbuckle.AspNetCore.Annotations;
using System;

namespace FMC.FIS.Business.Models.Customer
{

    /// <summary>
    /// Envio boleto por email
    /// </summary>
    [SwaggerSchema(Description = "Envio boleto por email")]
    public class SendEmailRequest
    {

        /// <summary>
        /// Id do produto (conta/contrato)
        /// </summary>
        [SwaggerSchema(Description = "Id do produto (conta/contrato)")]
        public long idProduct { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [SwaggerSchema(Description = "CPF")]
        public string cpf { get; set; }

        /// <summary>
        /// Numero conta/contrato
        /// </summary>
        [SwaggerSchema(Description = "Numero conta/contrato")]
        public string nrConta { get; set; }

        /// <summary>
        /// Codigo do boleto (enviar nulo)
        /// </summary>
        [SwaggerSchema(Description = "Codigo do boleto (enviar nulo)")]
        public string codBillet { get; set; }

        /// <summary>
        /// Numero da parcela do acordo (0 - para entrada)
        /// </summary>
        [SwaggerSchema(Description = "Numero da parcela do acordo (0 - para entrada)")]
        public int parcel { get; set; }

        /// <summary>
        /// Data vencimento do boleto
        /// </summary>
        [SwaggerSchema(Description = "Data vencimento do boleto")]
        public DateTime dtPayment { get; set; }

        /// <summary>
        /// Valor do boleto
        /// </summary>
        [SwaggerSchema(Description = "Valor do boleto")]
        public decimal vlBillet { get; set; }

        /// <summary>
        /// Linha digitavel
        /// </summary>
        [SwaggerSchema(Description = "Linha digitavel")]
        public string line { get; set; }

        /// <summary>
        /// Url do PDF do boleto
        /// </summary>
        [SwaggerSchema(Description = "Url do PDF do boleto")]
        public string urlPdf { get; set; }

        /// <summary>
        /// E-mail do cliente
        /// </summary>
        [SwaggerSchema(Description = "E-mail do cliente")]
        public string email { get; set; }

        /// <summary>
        /// Id do usuário 
        /// </summary>
        [SwaggerSchema(Description = "Id do usuário")]
        public int idUserLogin { get; set; }
    }

    public class SendSMSRequest
    {

        /// <summary>
        /// Id do produto (conta/contrato)
        /// </summary>
        [SwaggerSchema(Description = "Id do produto (conta/contrato)")]
        public long idProduct { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [SwaggerSchema(Description = "CPF")]
        public string cpf { get; set; }
        /// <summary>
        /// Codigo do boleto (enviar nulo)
        /// </summary>
        [SwaggerSchema(Description = "Codigo do boleto (enviar nulo)")]
        public string codBillet { get; set; }

        /// <summary>
        /// Numero da parcela
        /// </summary>
        [SwaggerSchema(Description = "Numero da parcela")]
        public int parcel { get; set; }

        /// <summary>
        /// Data vencimento do boleto
        /// </summary>
        [SwaggerSchema(Description = "Data vencimento do boleto")]
        public DateTime dtPayment { get; set; }

        /// <summary>
        /// Numero de telefone cliente
        /// </summary>
        [SwaggerSchema(Description = "Numero de telefone cliente")]
        public string phone { get; set; }

        /// <summary>
        /// Id do usuário 
        /// </summary>
        [SwaggerSchema(Description = "Id do usuário")]
        public int idUserLogin { get; set; }
    }
}
