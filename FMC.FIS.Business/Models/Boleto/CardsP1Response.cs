using System;
using System.Collections.Generic;

namespace FMC.FIS.Business.Models.Boleto
{
    public class CardsP1Response
    {
        public DateTime responseDate { get; set; }

        public string chaveRestart { get; set; }

        public ICollection<Cartoes> ListaCartoes { get; set; }

    }

    public class Cartoes
    {
        public CartaoP1Response Cartao { get; set; }
    }

    public class CartaoP1Response
    {
        private string cartao;
        public string nrCartao
        {
            get
            {
                string final = cartao.Substring(cartao.Length - 4, 4);
                string meio = "XXXXXX";
                string inicio = cartao.Substring(0, cartao.Length - 10);
                return inicio + meio + final;
            }
            set
            {
                cartao = value;
            }
        }
        public string nrToken { get; set; }
        public string cdStatusCartao { get; set; }
        public string nmPortadorCartao { get; set; }
        public string blkCode { get; set; }
        public string flgTitularidade { get; set; }
        public string dtExpiracao { get; set; }
        public string nrLogoCartao { get; set; }
        public string nrContaCartao { get; set; }
        public string cdStatusConta { get; set; }
        public string cdBloqueio1Conta { get; set; }
        public string cdBloqueio2Conta { get; set; }
        public string nrCpfCnpjCliente { get; set; }
        public string flgCartaoVirtual { get; set; }
        public string requestNumber { get; set; }

    }
}
