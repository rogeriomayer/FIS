using System;

namespace FMC.FIS.API.Models.Boleto
{
    public class CardP1Request
    {
        public string nrToken { get; set; }
    }

    public class CardP1Response
    {
        public DateTime responseDate { get; set; }
        public DadosCartao DadosCartao { get; set; }

        public DadosCliente DadosCliente { get; set; }
    }

    public class DadosCartao
    {
        public int logoCartao { get; set; }
        public string descCartao { get; set; }
        public Cartao Cartao { get; set; }
    }

    public class Cartao
    {
        public string nrCartao { get; set; }
        public string tkCartao { get; set; }
    }

    public class DadosCliente
    {
        public string nmCliente { get; set; }
        public string nmLogradouro { get; set; }
        public string nrLogradouro { get; set; }
        public string nmCompleLogradouro { get; set; }
        public string nmBairro { get; set; }
        public string nmCidade { get; set; }
        public string nmUF { get; set; }
        public string nrCep { get; set; }
        public string nrDocCliente { get; set; }
    }
}
