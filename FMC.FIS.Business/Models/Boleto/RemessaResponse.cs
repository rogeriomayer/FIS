using System;

namespace FMC.FIS.Business.Models.Boleto
{
    public class RemessasResponse
    {
        public string TipoProduto { get; set; }

        public DateTime DtGeracaoRemessa { get; set; }

        public DateTime? DtRetornoRemessa { get; set; }

        public long IdRemessa { get; set; }

        public string StatusRemessa { get; set; }

        public string NomeArquivo { get; set; }

        public long NrRemessa { get; set; }

    }

    public class RemessaResponse
    {

        public long IdRemessa { get; set; }

        public string NomeArquivo { get; set; }

        public byte[] File { get; set; }

    }


    public class Remessa
    {
        public string CaminhoArquivo { get; set; }

        public string Carteira { get; set; }

        public DateTime DtGeracaoRemessa { get; set; }

        public DateTime? DtRetornoRemessa { get; set; }

        public bool FlRegistrado { get; set; }

        public long IdRemessa { get; set; }

        public int IdStatusRemessa { get; set; }

        public string NomeArquivo { get; set; }

        public long NrRemessa { get; set; }

        public int NrRemessaDia { get; set; }

        public string SistemaOrigem { get; set; }

        public string UsuarioGeracao { get; set; }
    }
}
