using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FMC.FIS.Business.Models.Cobmais
{

    public class Contrato
    {
        [JsonProperty("id_contrato")]
        public long id_contrato { get; set; }

        [JsonProperty("negociacao_id")]
        public long negociacao_id { get; set; }

        [JsonProperty("credor_id")]
        public long credor_id { get; set; }

        [JsonProperty("credor_nome")]
        public string credor_nome { get; set; }

        [JsonProperty("negociacao_data")]
        public DateTime numero { get; set; }

        [JsonProperty("numero_contrato")]
        public string numero_contrato { get; set; }

        [JsonProperty("observacao")]
        public string observacao { get; set; }

        [JsonProperty("filial_id")]
        public string filial_id { get; set; }

        [JsonProperty("filial_descricao")]
        public string filial_descricao { get; set; }

        [JsonProperty("produto_id")]
        public string produto_id { get; set; }

        [JsonProperty("produto_descricao")]
        public string produto_descricao { get; set; }

        [JsonProperty("plano")]
        public long plano { get; set; }

        [JsonProperty("valor")]
        public decimal valor { get; set; }

        [JsonProperty("dados_adicionais")]
        public List<DadosAdicionais> dados_adicionais { get; set; }

        [JsonProperty("parcelas")]
        public List<Parcela> parcelas { get; set; }

    }
    public class DadosAdicionais
    {
        [JsonProperty("nome")]
        public string nome { get; set; }

        [JsonProperty("valor")]
        public string valor { get; set; }
    }

    public class Parcela
    {
        [JsonProperty("id")]
        public long id { get; set; }

        [JsonProperty("acordo_id")]
        public long acordo_id { get; set; }

        [JsonProperty("numero")]
        public string numero { get; set; }

        [JsonProperty("vencimento")]
        public DateTime vencimento { get; set; }

        [JsonProperty("valor")]
        public decimal valor { get; set; }

        [JsonProperty("observacao")]
        public string observacao { get; set; }

        [JsonProperty("dados_adicionais")]
        public List<object> dados_adicionais { get; set; }

    }

}
