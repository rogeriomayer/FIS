using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace FMC.FIS.Business.Models.Customer
{
    /// <summary>
    /// Dados saída simulação de acordo
    /// </summary>
    [SwaggerSchema(Description = "Dados saída simulação de acordo")]
    public class AgreementSimulateResponse
    {
        public AgreementSimulateResponse()
        {
            ParcelResponse = new HashSet<ParcelResponse>();
        }

        /// <summary>
        /// Data de entrada
        /// </summary>
        [SwaggerSchema(Description = "Data de entrada")]
        public DateTime DateEntrace { get; set; }

        /// <summary>
        /// Data da primeira parcela
        /// </summary>
        [SwaggerSchema(Description = "Data da primeira parcela")]
        public DateTime DateFirstParcel { get; set; }

        /// <summary>
        /// Valor em atraso
        /// </summary>
        [SwaggerSchema(Description = "Valor em atraso")]
        public decimal VlDue { get; set; }

        /// <summary>
        /// Valor em atraso atualizado
        /// </summary>
        [SwaggerSchema(Description = "Valor em atraso atualizado")]
        public decimal VlFull { get; set; }

        /// <summary>
        /// Valor dos juros
        /// </summary>
        [SwaggerSchema(Description = "Valor dos juros")]
        public decimal PctInterest { get; set; }

        /// <summary>
        /// Valor do desconto
        /// </summary>
        [SwaggerSchema(Description = "Valor do desconto")]
        public decimal PctDiscount { get; set; }

        /// <summary>
        /// Código da simulação
        /// </summary>
        [SwaggerSchema(Description = "Código da simulação")]
        public string CdSimulate { get; set; }

        /// <summary>
        /// Parcelamento
        /// </summary>
        [SwaggerSchema(Description = "Parcelamento")]
        public ICollection<ParcelResponse> ParcelResponse { get; set; }
    }

    /// <summary>
    /// Dados da parcela
    /// </summary>
    [SwaggerSchema(Description = "Dados da parcela")]
    public class ParcelResponse
    {
        /// <summary>
        /// Valor da entrada
        /// </summary>
        [SwaggerSchema(Description = "Valor da entrada")]
        public decimal ValueEntrace { get; set; }

        /// <summary>
        /// Numero de parcelas
        /// </summary>
        [SwaggerSchema(Description = "Numero de parcelas")]
        public int NrParcel { get; set; }

        /// <summary>
        /// Valor da parcela
        /// </summary>
        [SwaggerSchema(Description = "Valor da parcela")]
        public decimal VlParcel { get; set; }

        /// <summary>
        /// Valor do desconto
        /// </summary>
        [SwaggerSchema(Description = "Valor do desconto")]
        public decimal VlDiscount { get; set; }

        /// <summary>
        /// Percentual CET Mensal
        /// </summary>
        [SwaggerSchema(Description = "Percentual CET Mensal")]
        public decimal PctMonthCET { get; set; }

        /// <summary>
        /// Percentual CET Anual
        /// </summary>
        [SwaggerSchema(Description = "Percentual CET Anual")]
        public decimal PctYearCET { get; set; }

        /// <summary>
        /// Valor CET Mensal
        /// </summary>
        [SwaggerSchema(Description = "Valor CET Mensal")]
        public decimal VlMonthCET { get; set; }

        /// <summary>
        /// Valor CET Anual
        /// </summary>
        [SwaggerSchema(Description = "Valor CET Anual")]
        public decimal VlYearCET { get; set; }

        /// <summary>
        /// Valor total acordo
        /// </summary>
        [SwaggerSchema(Description = "Valor total acordo")]
        public decimal VlFull { get; set; }

        /// <summary>
        /// Data primeira parcela
        /// </summary>
        [SwaggerSchema(Description = "Data primeira parcela")]
        public DateTime DtParcel { get; set; }

        /// <summary>
        /// Código da parcela
        /// </summary>
        [SwaggerSchema(Description = "Codigo da parcela")]
        public string CdParcel { get; set; }
    }

    /// <summary>
    /// Dados entrada para simulação de acordo
    /// </summary>
    [SwaggerSchema(Description = "Dados entrada para simulação de acordo")]
    public class AgreementSimulateRequest
    {
        public AgreementSimulateRequest()
        {
            //ComplementData = new HashSet<ComplementData>();
            ParcelaCredz = new HashSet<ParcelaCredz>();
        }

        /// <summary>
        /// CPF cliente
        /// </summary>
        [SwaggerSchema(Description = "CPF cliente")]
        public string CPF { get; set; }

        /// <summary>
        /// Numero da conta/contrato do cliente
        /// </summary>
        [SwaggerSchema(Description = "Numero da conta/contrato do cliente")]
        public string Product { get; set; }

        /// <summary>
        /// Data da entrada acordo/promessa
        /// </summary>
        [SwaggerSchema(Description = "Data da entrada acordo/promessa")]
        public DateTime DtEntrace { get; set; }

        /// <summary>
        /// Valor da entrada acordo/promessa
        /// </summary>
        [SwaggerSchema(Description = "Valor da entrada acordo/promessa")]
        public decimal VlEntrace { get; set; }

        /// <summary>
        /// Desconto
        /// </summary>
        [SwaggerSchema(Description = "Valor do desconto")]
        public decimal PctDiscount { get; set; }

        /// <summary>
        /// Dias de atraso
        /// </summary>
        [SwaggerSchema(Description = "Dias de atraso")]
        public int Age { get; set; }

        /// <summary>
        /// Numero de parcelas
        /// </summary>
        [SwaggerSchema(Description = "Numero de parcelas")]
        public int NrParcel { get; set; }

        /// <summary>
        /// Código da simulação
        /// </summary>
        [SwaggerSchema(Description = "Código da simulação")]
        public string CdSimulate { get; set; }

        /// <summary>
        /// Código da simulação
        /// </summary>
        [SwaggerSchema(Description = "True: manter o mesmo valor de entrada para todas parcelas. False: Entrada variavel de acordo com o valor das parcelas")]
        public bool FixedEntraceValue { get; set; }

        /// <summary>
        /// Dados complementares
        /// </summary>
        [SwaggerSchema(Description = "Dados complementares")]
        //public ICollection<ComplementData> ComplementData { get; set; }
        public ICollection<ParcelaCredz> ParcelaCredz { get; set; }
    }

}
