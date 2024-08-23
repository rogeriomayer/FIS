using System;
using System.Collections.Generic;

namespace FMC.FIS.API.Models.Cobmais
{

    public class Contrato
    {
        public long id_contrato { get; set; }
        public long negociacao_id { get; set; }
        public long credor_id { get; set; }
        public string credor_nome { get; set; }
        public DateTime negociacao_data { get; set; }
        public string numero_contrato { get; set; }
        public string observacao { get; set; }
        public string filial_id { get; set; }
        public string filial_descricao { get; set; }
        public string produto_id { get; set; }
        public string produto_descricao { get; set; }
        public long plano { get; set; }
        public decimal valor { get; set; }
        public List<DadosAdicionais> dados_adicionais { get; set; }
        public List<Parcela> parcelas { get; set; }
    }
    public class DadosAdicionais
    {
        public string nome { get; set; }
        public string valor { get; set; }
    }

    public class Parcela
    {
        public long id { get; set; }
        public long acordo_id { get; set; }
        public string numero { get; set; }
        public DateTime vencimento { get; set; }
        public decimal valor { get; set; }
        public string observacao { get; set; }
        public List<object> dados_adicionais { get; set; }
    }

}
