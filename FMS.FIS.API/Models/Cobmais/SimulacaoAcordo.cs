using System;
using System.Collections.Generic;

namespace FMC.FIS.API.Models.Cobmais
{
    public class SimulacaoAcordoRequest
    {
        public DateTime data_calculo { get; set; }
        public int quantidade_parcelas { get; set; }
        public decimal valor_entrada { get; set; }
        public string forma_pagamento { get; set; }
        public List<ParcelaOriginal> parcelas_originais { get; set; }
        public Descontos descontos { get; set; }
    }

    public class Descontos
    {
        public decimal principal { get; set; }
        public decimal multa { get; set; }
        public decimal juros { get; set; }
        public decimal honorarios { get; set; }
        public bool desconto_maximo { get; set; }
        public bool desconto_relativo_campanha { get; set; }
    }

    public class ParcelaOriginal
    {
        public long negociacao_id { get; set; }
        public long id { get; set; }
        public string numero { get; set; }
        public DateTime vencimento { get; set; }
        public decimal valor { get; set; }
    }

    ////Response

    public class SimulacaoAcordoResponse
    {
        public AcordoSimulacao acordo { get; set; }
        public List<Opcoes> opcoes { get; set; }
    }

    public class AcordoSimulacao
    {
        public DateTime data_calculo { get; set; }
        public decimal taxa_parcelamento { get; set; }
        public decimal taxa_boleto { get; set; }
        public string assessoria_nome { get; set; }
        public decimal valorTotal { get; set; }
        public decimal valorTotalMinimo { get; set; }
        public List<ParcelasOriginaisSimulacao> parcelas_originais { get; set; }
        public List<ParcelasNovaSimulacao> parcelas_novas { get; set; }
        public string status_descricao { get; set; }
        public int status_id { get; set; }
    }

    public class Opcoes
    {
        public int numero_parcela { get; set; }
        public decimal percentual_minimo_entrada { get; set; }
        public decimal percentual_desconto_principal { get; set; }
        public decimal percentual_desconto_multa { get; set; }
        public decimal percentual_desconto_juros { get; set; }
        public decimal percentual_desconto_honorarios { get; set; }
    }

    public class ParcelasNovaSimulacao
    {
        public string numero { get; set; }
        public DateTime vencimento { get; set; }
        public decimal valor { get; set; }
    }

    public class ParcelasOriginaisSimulacao
    {
        public long negociacao_id { get; set; }
        public long id { get; set; }
        public decimal principal { get; set; }
        public decimal juros { get; set; }
        public decimal multa { get; set; }
        public decimal honorarios { get; set; }
        public decimal desconto_principal { get; set; }
        public decimal desconto_multa { get; set; }
        public decimal desconto_juros { get; set; }
        public decimal desconto_honorario { get; set; }
        public decimal total { get; set; }
        public decimal repasse_principal { get; set; }
        public decimal repasse_multa { get; set; }
        public decimal repasse_juros { get; set; }
        public decimal repasse_despesa { get; set; }
        public decimal repasse_total { get; set; }
    }

    public class BoletoResponse
    {

        public long id { get; set; }
        public long acordo_id { get; set; }
        public string nosso_numero { get; set; }
        public DateTime vencimento { get; set; }
        public decimal valor { get; set; }
        public string codigo_barra { get; set; }
        public string linha_digitavel { get; set; }
        public string url { get; set; }
    }

}
