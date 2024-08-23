using System;
using System.Collections.Generic;

namespace FMC.FIS.API.Models.Cobmais
{
    public class Pessoa
    {
        public long id_pessoa { get; set; }
        public string nome_pessoa { get; set; }
        public string cliente_cpfcnpj { get; set; }
        public DateTime data_nascimento { get; set; }
        public List<Telefone> telefones { get; set; }
        public List<Endereco> enderecos { get; set; }
        public List<Email> emails { get; set; }
        public List<Referencia> referencias { get; set; }
    }

    public class Telefone
    {
        public long id { get; set; }
        public int id_tipo { get; set; }
        public string numero { get; set; }
        public string observacao { get; set; }
        public bool ativo { get; set; }
        public bool contato { get; set; }
        public int id_pessoa_referencia { get; set; }
    }

    public class Email
    {
        public string id { get; set; }
        public string email { get; set; }
        public string observacao { get; set; }
        public bool ativo { get; set; }
        public long id_pessoa_referencia { get; set; }
    }

    public class Endereco
    {
        public long id { get; set; }
        public int id_tipo { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string cep { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public bool ativo { get; set; }
        public bool correspondencia { get; set; }
        public long id_pessoa_referencia { get; set; }
    }

    public class Referencia
    {
        public string contrato_numero { get; set; }
        public string id { get; set; }
        public string referencia_cpfcnpj { get; set; }
        public string referencia_nome { get; set; }
        public int id_tipo { get; set; }
        public List<Telefone> telefones { get; set; }
        public List<Endereco> enderecos { get; set; }
        public List<Email> emails { get; set; }
    }
}
