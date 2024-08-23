namespace FMC.FIS.Business.Models.OneB2K
{
    public class Devedor
    {
        public string vlTotalPrincipalRotativo { get; set; }
        public string vlTotalCtaFechado { get; set; }
        public string vlTotalAnuidadeCorrente { get; set; }
        public string vlTotalSeguros { get; set; }
        public string vlTotalDebitos { get; set; }
        public string vlTotalEncargosFatura { get; set; }
        public string vlTotalParceladoLojaVencer { get; set; }
        public string vlTotalTaxaServico { get; set; }
        public string vlTotalPricipalFaturados { get; set; }
        public string vlTotalJurosRotativos { get; set; }
        public string jurosPermanencia { get; set; }
        public string multa { get; set; }
        public string vlTotalEncargos { get; set; }
        public string iofCorrente { get; set; }
        public string vlTotalEncargosRotativo { get; set; }
        public string vlTotalTarifas { get; set; }
        public string vlTotalEncargosCorrente { get; set; }
        public string vlTotalPrincipal { get; set; }
        public string vlTotalTarifasRotativo { get; set; }
        public string vlTotalTaxaServicoRotativo { get; set; }
        public string vlTotalTaxaServicoCorrente { get; set; }
        public string vlTotalTarifasFaturas { get; set; }
        public string vlConsolidadoFaturaVencida { get; set; }
        public string jurosCa { get; set; }
        public string iofRotativoAnt { get; set; }
        public string vlTotalAnuidadeRotativo { get; set; }
        public string vlTotalPrincipalCorrente { get; set; }
        public string vlTotalSaldoInicial { get; set; }
        public string vlTotalAnuidadeFaturas { get; set; }
        public string vlEncargosCaTotal { get; set; }
        public string vlEncargosCaFaturados { get; set; }
        public string vlTotalCreditos { get; set; }
        public string vlTotalSegurosCorrentes { get; set; }
        public string vlTotalFechadoContaDesagio { get; set; }
        public string vlEncargosCaCorrentes { get; set; }
        public string vlTotalTaxaServicoFatura { get; set; }
        public string vlTotalSaldoCorrente { get; set; }
        public string vlTotalSegurosFaturados { get; set; }
        public string vlTotalSeguroRotativo { get; set; }
        public string vlrTotalParcelasVencidasEmissor { get; set; }
        public string vlTotalTarifasCorrentes { get; set; }
        public string jurosMora { get; set; }
        public string vlEncargosCaVencidos { get; set; }
        public string vlTotalAnuidades { get; set; }
    }

    public class DevedorResponse
    {
        public Devedor responseData { get; set; }
        public Status status { get; set; }
    }
}
