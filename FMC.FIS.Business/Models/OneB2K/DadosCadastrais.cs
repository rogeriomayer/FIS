namespace FMC.FIS.Business.Models.OneB2K
{

    public class DadosCadastraisRespose
    {
        public Status status { get; set; }
        public DadosCadastrais responseData { get; set; }
    }

    public class InformacoesGerais
    {
        public string idTipo { get; set; }
        public string nrConta { get; set; }
        public string nmEmbossed { get; set; }
        public string codProduto { get; set; }
        public string codSubProduto { get; set; }
        public string codBloqueio { get; set; }
        public string howStolen { get; set; }
        public string dtUltimoBloqueio { get; set; }
        public string indClienteVIP { get; set; }
        public int vlLimiteCredito { get; set; }
        public double vlLimiteCreditoDisponivel { get; set; }
        public int vlLimiteSaque { get; set; }
        public double vlSaldoAnterior { get; set; }
        public double vlSaldoAtual { get; set; }
        public double vlAutorizacoesPendentes { get; set; }
        public double vlSaldoParceladoPendente { get; set; }
        public double vlComprasFaturar { get; set; }
        public string diaVencimento { get; set; }
        public string flagUnificado { get; set; }
        public string tpProcessamento { get; set; }
        public string cancelReason { get; set; }
        public string historicoVencimento { get; set; }
        public string dtManutencaoUltimoArquivo { get; set; }
        public string hrManutencaoUltimoArquivo { get; set; }
        public int qtdCorrespDevolvida { get; set; }
        public string dispensaAnuidade { get; set; }
        public double limiteSaqueUtilizado { get; set; }
        public double limiteSaqueDisponivel { get; set; }
        public string debito { get; set; }
        public string dtUltimoPagamento { get; set; }
        public double vlrUltimoPagamento { get; set; }
        public double vlrAtraso { get; set; }
        public double vlrDisputa { get; set; }
        public string dtCorte { get; set; }
        public string tipoCliente { get; set; }
        public string indExcecao { get; set; }
        public string flagErroEndereco { get; set; }
        public string optIn { get; set; }
        public string taxaDifParcFat { get; set; }
        public string taxaDifParcAut { get; set; }
        public string envioFatura { get; set; }
    }

    public class DadosCadastrais
    {
        public string produto { get; set; }
        public string nome1 { get; set; }
        public string dtNascimento1 { get; set; }
        public string nrDocumento1 { get; set; }
        public string idSexo1 { get; set; }
        public string idEstadoCivil1 { get; set; }
        public string fone1 { get; set; }
        public string fone2 { get; set; }
        public string fone3 { get; set; }
        public string nome2 { get; set; }
        public string nrDocumento2 { get; set; }
        public string idSexo2 { get; set; }
        public string idEstadoCivil2 { get; set; }
        public string nome3 { get; set; }
        public string nrDocumento3 { get; set; }
        public string idSexo3 { get; set; }
        public string idEstadoCivil3 { get; set; }
        public string nome4 { get; set; }
        public string nrDocumento4 { get; set; }
        public string idSexo4 { get; set; }
        public string idEstadoCivil4 { get; set; }
        public int diasAtraso { get; set; }
        public string email { get; set; }
        public string documentoRg { get; set; }
        public string enderecoAntigo { get; set; }
        public string ultimaAtualizacaoEndereco { get; set; }
        public string agencia { get; set; }
        public string contaCorrente { get; set; }
        public string dataAberturaConta { get; set; }
        public string nomeMae { get; set; }
        public string nomePai { get; set; }
        public string dvu { get; set; }
        public string tipoCartao1 { get; set; }
        public string tipoCartao2 { get; set; }
        public Endereco endereco { get; set; }
        public EnderecoFatura enderecoFatura { get; set; }
        public InformacoesGerais informacoesGerais { get; set; }
    }

}
