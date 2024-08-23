using System;
using System.Collections.Generic;

namespace FMC.FIS.Business.Models.Cobmais
{
    public class PagamentoRequest
    {
        public DateTime data_processamento_inicial { get; set; }
        public DateTime data_processamento_final { get; set; }
        public DateTime data_pagamento_inicial { get; set; }
        public DateTime data_pagamento_final { get; set; }
        public string id_acordo { get; set; }
        public string id_importacao_cnab { get; set; }
    }


    public class PagamentoResponse
    {
        public PagamentoResponse()
        {
            pagamentos = new HashSet<Pagamento>();
        }
        public string cpfcnpj { get; set; }
        public string codigo { get; set; }
        public ICollection<Pagamento> pagamentos { get; set; }
    }

    public class Pagamento
    {
        public Pagamento()
        {
            parcelas = new HashSet<ParcelaPagamento>();
            boletos = new HashSet<BoletoCompleto>();
        }
        public long id { get; set; }
        public string data_processamento { get; set; }
        public string data_pagamento { get; set; }
        public string forma_pagamento { get; set; }
        public string observacoes { get; set; }
        public int id_importacao_cnab { get; set; }
        public long id_acordo { get; set; }
        public string codigo_acordo { get; set; }
        public string numero_acordo { get; set; }
        public decimal valor_pagamento { get; set; }
        public ICollection<ParcelaPagamento> parcelas { get; set; }
        public ICollection<BoletoCompleto> boletos { get; set; }

    }

    public class ParcelaPagamento
    {
        public int id { get; set; }
        public string numero { get; set; }
        public int id_contrato { get; set; }
        public string numero_contrato { get; set; }
        public string vencimento { get; set; }
        public decimal valor { get; set; }
    }




}
