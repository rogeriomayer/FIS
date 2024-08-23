using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace FMC.FIS.Business.Models.Cobmais
{
    /// <summary>
    /// Dados Acordo Cobmais
    /// </summary>
    [SwaggerSchema(Description = "Dados Acordo Cobmais")]
    public class Acordo
    {

        /// <summary>
        /// Id Acordo
        /// </summary>
        [SwaggerSchema(Description = "Id Acordo")]
        [JsonProperty("id")]
        public long id { get; set; }

        /// <summary>
        /// Status acordo
        /// </summary>
        [SwaggerSchema(Description = "Status acordo")]
        [JsonProperty("status_descricao")]
        public string status_descricao { get; set; }

        /// <summary>
        /// Status acordo
        /// </summary>
        [SwaggerSchema(Description = "Status acordo")]
        [JsonProperty("status")]
        public string status { get; set; }

        /// <summary>
        /// Id Status acordo
        /// </summary>
        [SwaggerSchema(Description = "Id Status acordo")]
        [JsonProperty("status_id")]
        public Nullable<int> status_id { get; set; }

        /// <summary>
        /// Id Status acordo
        /// </summary>
        [SwaggerSchema(Description = "Id Status acordo")]
        [JsonProperty("id_status")]
        public Nullable<int> id_status { get; set; }

        /// <summary>
        /// Numero acordo
        /// </summary>
        [SwaggerSchema(Description = "Numero acordo")]
        [JsonProperty("numero")]
        public string numero { get; set; }

        /// <summary>
        /// Data Entrada
        /// </summary>
        [SwaggerSchema(Description = "Data Entrada")]
        [JsonProperty("data")]
        public DateTime data { get; set; }

        /// <summary>
        /// Quantidade de parcelas
        /// </summary>
        [SwaggerSchema(Description = "Quantidade de parcelas")]
        [JsonProperty("quantidade_parcelas")]
        public int quantidade_parcelas { get; set; }

        /// <summary>
        /// Valor do principal
        /// </summary>
        [SwaggerSchema(Description = "Valor do principal")]
        [JsonProperty("principal")]
        public decimal principal { get; set; }

        /// <summary>
        /// Valor da multa
        /// </summary>
        [SwaggerSchema(Description = "Valor da multa")]
        [JsonProperty("multa")]
        public decimal multa { get; set; }

        /// <summary>
        /// Valor dos juros
        /// </summary>
        [SwaggerSchema(Description = "Valor dos juros")]
        [JsonProperty("juros")]
        public decimal juros { get; set; }

        /// <summary>
        /// Valor dos honorarios
        /// </summary>
        [SwaggerSchema(Description = "Valor dos honorarios")]
        [JsonProperty("honorarios")]
        public decimal honorarios { get; set; }

        /// <summary>
        /// Valor das despesas
        /// </summary>
        [SwaggerSchema(Description = "Valor das despesas")]
        [JsonProperty("despesas")]
        public decimal despesas { get; set; }

        /// <summary>
        /// Taxa de parcelamento
        /// </summary>
        [SwaggerSchema(Description = "Taxa de parcelamento")]
        [JsonProperty("taxa_parcelamento")]
        public decimal taxa_parcelamento { get; set; }

        /// <summary>
        /// Desconto no principal
        /// </summary>
        [SwaggerSchema(Description = "Desconto no principal")]
        [JsonProperty("desconto_principal")]
        public decimal desconto_principal { get; set; }

        /// <summary>
        /// Desconto na multa
        /// </summary>
        [SwaggerSchema(Description = "Desconto na multa")]
        [JsonProperty("desconto_multa")]
        public decimal desconto_multa { get; set; }

        /// <summary>
        /// Desconto nos juros
        /// </summary>
        [SwaggerSchema(Description = "Desconto nos juros")]
        [JsonProperty("desconto_juros")]
        public decimal desconto_juros { get; set; }

        /// <summary>
        /// Desconto nos honorarios
        /// </summary>
        [SwaggerSchema(Description = "Desconto nos honorarios")]
        [JsonProperty("desconto_honorario")]
        public decimal desconto_honorario { get; set; }

        /// <summary>
        /// Valor total acordo
        /// </summary>
        [SwaggerSchema(Description = "Valor total acordo")]
        [JsonProperty("total")]
        public decimal total { get; set; }

        /// <summary>
        /// Valor repasse no principal
        /// </summary>
        [SwaggerSchema(Description = "Valor repasse no principal")]
        [JsonProperty("repasse_principal")]
        public decimal repasse_principal { get; set; }

        /// <summary>
        /// Valor repasse de multa
        /// </summary>
        [SwaggerSchema(Description = "Valor repasse de multa")]
        [JsonProperty("repasse_multa")]
        public decimal repasse_multa { get; set; }


        /// <summary>
        /// Valor repasse de juros
        /// </summary>
        [SwaggerSchema(Description = "Valor repasse de juros")]
        [JsonProperty("repasse_juros")]
        public decimal repasse_juros { get; set; }

        /// <summary>
        /// Valor repasse de despesas
        /// </summary>
        [SwaggerSchema(Description = "Valor repasse de despesas")]
        [JsonProperty("repasse_despesas")]
        public decimal repasse_despesas { get; set; }

        /// <summary>
        /// Valor repasse de taxa de parcelamento
        /// </summary>
        [SwaggerSchema(Description = "Valor repasse de taxa de parcelamento")]
        [JsonProperty("repasse_taxa_parcelamento")]
        public decimal repasse_taxa_parcelamento { get; set; }

        /// <summary>
        /// Valor repasse total
        /// </summary>
        [SwaggerSchema(Description = "Valor repasse total")]
        [JsonProperty("total_repasse")]
        public decimal total_repasse { get; set; }

        /// <summary>
        /// Operador
        /// </summary>
        [SwaggerSchema(Description = "Operador")]
        [JsonProperty("operador_vinculado")]
        public string operador_vinculado { get; set; }


        /// <summary>
        /// Valor parcial
        /// </summary>
        [SwaggerSchema(Description = "Valor parcial")]
        [JsonProperty("parcial")]
        public bool parcial { get; set; }

        /// <summary>
        /// Parcelas originais
        /// </summary>
        [SwaggerSchema(Description = "Parcelas originais")]
        [JsonProperty("parcelas_originais")]
        public List<ParcelasOriginal> parcelas_originais { get; set; }

        /// <summary>
        /// Parcelas novas
        /// </summary>
        [SwaggerSchema(Description = "Parcelas novas")]
        [JsonProperty("parcelas_novas")]
        public List<ParcelasNova> parcelas_novas { get; set; }

        /// <summary>
        /// Boletos
        /// </summary>
        [SwaggerSchema(Description = "Boletos")]
        [JsonProperty("boletos")]
        public List<BoletoSimples> boletos { get; set; }
    }

    /// <summary>
    /// Boleto
    /// </summary>
    [SwaggerSchema(Description = "Parcelas originais")]
    public class BoletoSimples
    {

        /// <summary>
        /// Id do boleto
        /// </summary>
        [SwaggerSchema(Description = "Id do boleto")]
        [JsonProperty("id")]
        public long id { get; set; }

        /// <summary>
        /// Id do acordo
        /// </summary>
        [SwaggerSchema(Description = "Id do acordo")]
        [JsonProperty("acordo_id")]
        public long acordo_id { get; set; }

        /// <summary>
        /// Nosso número do boleto
        /// </summary>
        [SwaggerSchema(Description = "Nosso numero do boleto")]
        [JsonProperty("nosso_numero")]
        public string nosso_numero { get; set; }

        /// <summary>
        /// Data de vencimento do boleto
        /// </summary>
        [SwaggerSchema(Description = "Data de vencimento do boleto")]
        [JsonProperty("vencimento")]
        public DateTime vencimento { get; set; }

        /// <summary>
        /// Valor do boleto
        /// </summary>
        [SwaggerSchema(Description = "Valor do boleto")]
        [JsonProperty("valor")]
        public decimal valor { get; set; }

        /// <summary>
        /// Codigo de barras
        /// </summary>
        [SwaggerSchema(Description = "Codigo de barras")]
        [JsonProperty("codigo_barra")]
        public string codigo_barra { get; set; }

        /// <summary>
        /// Linha digitavel
        /// </summary>
        [SwaggerSchema(Description = "Linha digitavel")]
        [JsonProperty("linha_digitavel")]
        public string linha_digitavel { get; set; }

        /// <summary>
        /// URL do PDF do boleto
        /// </summary>
        [SwaggerSchema(Description = "URL do boleto")]
        [JsonProperty("url")]
        public string url { get; set; }
    }

    /// <summary>
    /// Parcela do Acordo
    /// </summary>
    [SwaggerSchema(Description = "Parcela do Acordo")]
    public class ParcelasNova
    {

        /// <summary>
        /// Id do Acordo
        /// </summary>
        [SwaggerSchema(Description = "Id do Acordo")]
        [JsonProperty("id")]
        public long id { get; set; }

        /// <summary>
        /// Numero da parcela
        /// </summary>
        [SwaggerSchema(Description = "Numero da parcela")]
        [JsonProperty("parcela")]
        public string parcela { get; set; }

        /// <summary>
        /// Data vencimento 
        /// </summary>
        [SwaggerSchema(Description = "Data de vencimento")]
        [JsonProperty("vencimento")]
        public DateTime vencimento { get; set; }

        /// <summary>
        /// Valor da parcela
        /// </summary>
        [SwaggerSchema(Description = "Valor da parcela")]
        [JsonProperty("valor")]
        public decimal valor { get; set; }

        /// <summary>
        /// Id do contrato
        /// </summary>
        [SwaggerSchema(Description = "Id do contrato")]
        [JsonProperty("id_contrato")]
        public Nullable<long> id_contrato { get; set; }

        /// <summary>
        /// Data de pagamento da parcela
        /// </summary>
        [SwaggerSchema(Description = "Data de pagamento da parcela")]
        [JsonProperty("data_pagamento")]
        public Nullable<DateTime> data_pagamento { get; set; }

        /// <summary>
        /// Id do pagamento
        /// </summary>
        [SwaggerSchema(Description = "Id do pagamento")]
        [JsonProperty("id_pagamento")]
        public Nullable<long> id_pagamento { get; set; }
    }


    /// <summary>
    /// Parcela original
    /// </summary>
    [SwaggerSchema(Description = "Parcela original")]
    public class ParcelasOriginal
    {
        /// <summary>
        /// Id da parcela original
        /// </summary>
        [SwaggerSchema(Description = "Id da parcela original")]
        [JsonProperty("id")]
        public long id { get; set; }

        /// <summary>
        /// Dias atraso
        /// </summary>
        [SwaggerSchema(Description = "Dias atrao")]
        [JsonProperty("atraso")]
        public int atraso { get; set; }

        /// <summary>
        /// Valor do principal
        /// </summary>
        [SwaggerSchema(Description = "Valor do pricipal")]
        [JsonProperty("principal")]
        public decimal principal { get; set; }

        /// <summary>
        /// Valor dos juros
        /// </summary>
        [SwaggerSchema(Description = "Valor dos juros")]
        [JsonProperty("juros")]
        public decimal juros { get; set; }

        /// <summary>
        /// Valor da multa
        /// </summary>
        [SwaggerSchema(Description = "Valor da multa")]
        [JsonProperty("multa")]
        public decimal multa { get; set; }

        /// <summary>
        /// Valor dos honorarios
        /// </summary>
        [SwaggerSchema(Description = "Valor dos honorarios")]
        [JsonProperty("honorarios")]
        public decimal honorarios { get; set; }

        /// <summary>
        /// Desconto no principal
        /// </summary>
        [SwaggerSchema(Description = "Desconto no principal")]
        [JsonProperty("desconto_principal")]
        public decimal desconto_principal { get; set; }

        /// <summary>
        /// Desconto na multa
        /// </summary>
        [SwaggerSchema(Description = "Desconto na multa")]
        [JsonProperty("desconto_multa")]
        public decimal desconto_multa { get; set; }

        /// <summary>
        /// Desconto nos juros
        /// </summary>
        [SwaggerSchema(Description = "Desconto nos juros")]
        [JsonProperty("desconto_juros")]
        public decimal desconto_juros { get; set; }

        /// <summary>
        /// Desconto nos honorarios
        /// </summary>
        [SwaggerSchema(Description = "Desconto nos honorarios")]
        [JsonProperty("desconto_honorarios")]
        public decimal desconto_honorarios { get; set; }

        /// <summary>
        /// Desconto nas despesas
        /// </summary>
        [SwaggerSchema(Description = "Desconto nas despesas")]
        [JsonProperty("despesas")]
        public decimal despesas { get; set; }

        /// <summary>
        /// Valor total
        /// </summary>
        [SwaggerSchema(Description = "Valor total")]
        [JsonProperty("total")]
        public decimal total { get; set; }

        /// <summary>
        /// Valor de repasse total
        /// </summary>
        [SwaggerSchema(Description = "Valor de repasse total")]
        [JsonProperty("repasse_principal")]
        public decimal repasse_principal { get; set; }

        /// <summary>
        /// Valor repasse multa
        /// </summary>
        [SwaggerSchema(Description = "Valor repasse multa")]
        [JsonProperty("repasse_multa")]
        public decimal repasse_multa { get; set; }

        /// <summary>
        /// Valor repasse de juros
        /// </summary>
        [SwaggerSchema(Description = "Valor repasse juros")]
        [JsonProperty("repasse_juros")]
        public decimal repasse_juros { get; set; }

        /// <summary>
        /// Valor repasse despesas
        /// </summary>
        [SwaggerSchema(Description = "Valor repasse despesas")]
        [JsonProperty("repasse_despesa")]
        public decimal repasse_despesa { get; set; }

        /// <summary>
        /// Valor total de repasse
        /// </summary>
        [SwaggerSchema(Description = "Valor repasse total")]
        [JsonProperty("total_repasse")]
        public decimal total_repasse { get; set; }
    }

    public class BoletoCompleto
    {
        [JsonProperty("id")]
        public long id { get; set; }

        [JsonProperty("acordo_id")]
        public long acordo_id { get; set; }

        [JsonProperty("status")]
        public bool status { get; set; }

        [JsonProperty("data_documento")]
        public DateTime data_documento { get; set; }

        [JsonProperty("numero_documento")]
        public string numero_documento { get; set; }

        [JsonProperty("numero_parcela")]
        public string numero_parcela { get; set; }

        [JsonProperty("vencimento")]
        public DateTime vencimento { get; set; }

        [JsonProperty("valor")]
        public decimal valor { get; set; }

        [JsonProperty("nosso_numero")]
        public string nosso_numero { get; set; }

        [JsonProperty("codigo_barra")]
        public string codigo_barra { get; set; }

        [JsonProperty("linha_digitavel")]
        public string linha_digitavel { get; set; }

        [JsonProperty("instrucoes")]
        public string instrucoes { get; set; }

        [JsonProperty("mensagem")]
        public string mensagem { get; set; }

        [JsonProperty("codigo_banco")]
        public string codigo_banco { get; set; }

        [JsonProperty("carteira")]
        public string carteira { get; set; }

        [JsonProperty("cedente")]
        public string cedente { get; set; }

        [JsonProperty("digito_cedente")]
        public string digito_cedente { get; set; }

        [JsonProperty("agencia")]
        public string agencia { get; set; }

        [JsonProperty("digito_agencia")]
        public string digito_agencia { get; set; }

        [JsonProperty("conta_corrente")]
        public string conta_corrente { get; set; }

        [JsonProperty("digito_conta_corrente")]
        public string digito_conta_corrente { get; set; }

        [JsonProperty("especie")]
        public string especie { get; set; }

        [JsonProperty("moeda")]
        public string moeda { get; set; }

        [JsonProperty("aceite")]
        public string aceite { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }
    }
}
