using System;
using System.Collections.Generic;

namespace FMC.WebSite.FIS.Models
{
   
    public class ParcelamentoFaturaResponse
    {
        public ParcelamentoFaturaData responseData { get; set; }
    }

    public class ParcelamentoFaturaData
    {
        public ParcelamentoFaturaData()
        {
            listaParcelamentoOpcao = new HashSet<ParcelamentoOpcao>();
        }
        public string tipoParcelado { get; set; }
        public string valorAdesao { get; set; }
        public string valorParcelado { get; set; }
        public string subProduto { get; set; }
        public string categoria { get; set; }
        public string totalTransacao { get; set; }
        public string saldoFatura { get; set; }
        public string vencimento { get; set; }
        public string utilizaCred { get; set; }
        public string billingCode { get; set; }
        public string parcelas { get; set; }
        public string cliente { get; set; }
        public ICollection<ParcelamentoOpcao> listaParcelamentoOpcao { get; set; }
        public string produto { get; set; }
        public string diasAtraso { get; set; }
        public string contratosAbertos { get; set; }
        public string canal { get; set; }
        public string valorMinimo { get; set; }
        public string status { get; set; }
        public string blockReclass { get; set; }
    }

    public class ParcelamentoOpcao
    {
        public string opcao { get; set; }
        public string valorParcela { get; set; }
        public string iofDiario { get; set; }
        public string cetAno { get; set; }
        public string valorJuros { get; set; }
        public string quantidadeParcelas { get; set; }
        public string taxas { get; set; }
        public string cetMes { get; set; }
        public string iofAdicional { get; set; }
    }


    public class ParcelamentoFaturaRequest
    {
        public ParcelamentoOpcao opcao { get; set; }
        public ParcelamentoFaturaData dados { get; set; }
        public string acao { get; set; }
    }

}
