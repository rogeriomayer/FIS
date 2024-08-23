using System.Collections.Generic;

namespace FMC.FIS.API.Models.OneB2K
{
    public class Portador
    {
        public PortadorData responseData { get; set; }
        public Status status { get; set; }
    }


    public class PortadorData
    {
        public string chaveRestart { get; set; }
        public ICollection<ListaPortadores> listaPortadores { get; set; }
    }
    public class Cartao
    {
        public string nrCartao { get; set; }
        public string jwtCartao { get; set; }
    }

    public class EnderecoPortador
    {
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
    }

    public class ListaPortadores
    {
        public string tpRegistro { get; set; }
        public string nrConta { get; set; }
        public string nmPortador { get; set; }
        public EnderecoPortador enderecoPortador { get; set; }
        public string cdBloqueio { get; set; }
        public string cdClassificacaoBloqueio { get; set; }
        public Cartao cartao { get; set; }
    }


    public class Status
    {
        public string responseId { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }

}
