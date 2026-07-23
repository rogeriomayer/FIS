using System;
using System.Collections.Generic;

namespace FMC.FIS.Business.Models.OneB2K
{
    public class ResumoFatura
    {
        public ResumoFaturaData responseData { get; set; }
        public Status status { get; set; }
    }

    public class ResumoFaturaData
    {
        public Limites limites { get; set; }
        public Taxas taxas { get; set; }
        public double vlSaldoFaturaAnteriorReal { get; set; }
        public double vlTotalPagamento { get; set; }
        public double vlTotalOutrosCreditos { get; set; }
        public double vlTotalOutrosDebitos { get; set; }
        public double vlTotalEncargos { get; set; }
        public double vlTotalCompraReal { get; set; }
        public double vlTotalDolarConvertidoReal { get; set; }
        public double vlSaldoTotalFatura { get; set; }
        public double vlPagamentoMinimo { get; set; }
        public double vlCotacaoDolar { get; set; }
        public string dtMelhorCompra { get; set; }
        public double vlTotalCreditoPagamento { get; set; }
        public double vlTotalSaqueReal { get; set; }
    }

    public class Taxas
    {
        public double vlTxCETRotativoMensal { get; set; }
        public double vlTxCETRotativoAnual { get; set; }
        public double vlTxRotativoProx { get; set; }
    }

    public class Limites
    {
        public double vlLimiteCompra { get; set; }
    }


    public class IdFaturaPDFResponse
    {
        public IdFaturaPDF responseData { get; set; }
        public Status status { get; set; }
    }
    public class IdFaturaPDF
    {
        public int totalRegistros { get; set; }
        public ICollection<ListaIdPDF> listaPDF { get; set; }
    }

    public class ListaIdPDF
    {
        public string idPDF { get; set; }
        public DateTime dtVencimento { get; set; }
        public DateTime dtMovimento { get; set; }
    }

    public class LinhaDigitavelResponse
    {
        public LinhaDigitavelRet responseData { get; set; }
        public Status status { get; set; }
    }

    public class LinhaDigitavelRet
    {
        public string linhaDigitavel { get; set; }
    }

    
}
