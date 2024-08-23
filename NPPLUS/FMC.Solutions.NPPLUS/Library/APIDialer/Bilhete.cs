using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.APIDialer
{
    public class Bilhete
    {
        public int IdPausa { get; set; }

        public string IdCall { get; set; }

        public string Conta { get; set; }

        public string CPF { get; set; }

        public string NumeroTelefone { get; set; }

        public TipoChamada? TipoChamada { get; set; }

        public string Campanha { get; set; }

        public string DescricaoStatus { get; set; }

        public Status Status { get; set; }

        public string NomeArquivoAudio { get; set; }

    }

    public enum Status
    {
        Liberado,
        Deslogado,
        Pausa,
        EmChamada,
        EmFilaEspera,
        Erro
    }

    public enum TipoChamada
    {
        Aguardando,
        Outbound,
        Inbound,
        Manual,
        Transferencia
    }
}
