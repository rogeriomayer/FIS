using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FMC.FIS.Business.Models.Cobmais
{
    public class Pessoa
    {
        [JsonProperty("id_pessoa")]
        public long id_pessoa { get; set; }
        
        [JsonProperty("nome_pessoa")]
        public string nome_pessoa { get; set; }

        [JsonProperty("cliente_cpfcnpj")]
        public string cliente_cpfcnpj { get; set; }

        [JsonProperty("cliente_rg")]
        public string rg { get; set; }

        [JsonProperty("data_nascimento")]
        public DateTime data_nascimento { get; set; }

        [JsonProperty("telefones")]
        public List<Telefone> telefones { get; set; }

        [JsonProperty("enderecos")]
        public List<Endereco> enderecos { get; set; }

        [JsonProperty("emails")]
        public List<Email> emails { get; set; }

        [JsonProperty("referencia")]
        public List<Referencia> referencias { get; set; }
    }

    public class Telefone
    {
        [JsonProperty("id")]
        public long id { get; set; }

        [JsonProperty("id_tipo")]
        public int id_tipo { get; set; }

        [JsonProperty("numero")]
        public string numero { get; set; }

        [JsonProperty("observacao")]
        public string observacao { get; set; }

        [JsonProperty("ativo")]
        public bool ativo { get; set; }

        [JsonProperty("contato")]
        public bool contato { get; set; }

        [JsonProperty("id_pessoa_referencia")]
        public int id_pessoa_referencia { get; set; }
    }

    public class Email
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("obs")]
        public string observacao { get; set; }

        [JsonProperty("ativo")]
        public bool ativo { get; set; }

        [JsonProperty("idPessoaReferencia")]
        public long id_pessoa_referencia { get; set; }
    }

    public class Endereco
    {
        [JsonProperty("id")]
        public long id { get; set; }

        [JsonProperty("id_tipo")]
        public int id_tipo { get; set; }

        [JsonProperty("logradouro")]
        public string logradouro { get; set; }

        [JsonProperty("numero")]
        public string numero { get; set; }

        [JsonProperty("complemento")]
        public string complemento { get; set; }

        [JsonProperty("cep")]
        public string cep { get; set; }

        [JsonProperty("bairro")]
        public string bairro { get; set; }

        [JsonProperty("nome")]
        public string cidade { get; set; }

        [JsonProperty("uf")]
        public string uf { get; set; }

        [JsonProperty("ativo")]
        public bool ativo { get; set; }

        [JsonProperty("correspondencia")]
        public bool correspondencia { get; set; }

        [JsonProperty("id_pessoa_referencia")]
        public long id_pessoa_referencia { get; set; }
    }


    public class Referencia
    {
        [JsonProperty("contrato_numero")]
        public string contrato_numero { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("referencia_cpfcnpj")]
        public string referencia_cpfcnpj { get; set; }

        [JsonProperty("referencia_nome")]
        public string referencia_nome { get; set; }

        [JsonProperty("id_tipo")]
        public int id_tipo { get; set; }

        [JsonProperty("telefones")]
        public List<Telefone> telefones { get; set; }

        [JsonProperty("enderecos")]
        public List<Endereco> enderecos { get; set; }

        [JsonProperty("emails")]
        public List<Email> emails { get; set; }
    }

    public class ChaveCredor
    {

        [JsonProperty("chave_cliente")]
        public string chave_cliente { get; set; }

        [JsonProperty("nome_credor")]
        public string nome_credor { get; set; }
    }

    public class EnderecoRequest
    {
        [JsonProperty("id_pessoa")]
        public long id_pessoa { get; set; }

        [JsonProperty("endereco")]
        public Endereco endereco { get; set; }
    }
}
