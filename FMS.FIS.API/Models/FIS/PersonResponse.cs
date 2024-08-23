using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.FIS.API.Models.FIS
{
    public class PersonResponse
    {
        public PersonResponse()
        {
            Phones = new HashSet<Phones>();
            Emails = new HashSet<Emails>();
            Accounts = new HashSet<Account>();
        }

        public long IdPerson { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public Addresses Address { get; set; }
        public ICollection<Phones> Phones { get; set; }
        public ICollection<Emails> Emails { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }

    public class Emails
    {
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }

    public class Addresses
    {
        public long IdAddress { get; set; }
        public string Cep { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string UF { get; set; }
    }
    public class Phones
    {
        public long IdPhone { get; set; }
        public string PhoneNumber { get; set; }
        public int PhoneType { get; set; }
    }


    public class Account
    {
        public Account()
        {
            DayPayment = new HashSet<int>();
        }

        public string AccountNumber { get; set; }

        public string CardNumber { get; set; }

        public string Product { get; set; }

        public DateTime DueDate { get; set; }

        public int Age { get; set; }

        public decimal Balance { get; set; }

        public decimal MinimumPayment { get; set; }

        public string CardImage { get; set; }

        public string AccountType { get; set; }

        public int ORG { get; set; }

        public int LOGO { get; set; }

        public ICollection<int> DayPayment { get; set; }

    }

}
namespace FMC.FIS.Model.Pessoa
{
    public class Pessoa
    {

        public Pessoa()
        {
            Telefone = new HashSet<Telefone>();
            Emails = new HashSet<Emails>();
            Contas = new HashSet<Conta>();
        }

        public long IdPessoa { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public Endereco Endereco { get; set; }
        public ICollection<Telefone> Telefone { get; set; }
        public ICollection<Emails> Emails { get; set; }
        public ICollection<Conta> Contas { get; set; }
    }

    public class Emails
    {
        public string Email { get; set; }
        public DateTime Data { get; set; }
    }

    public class Endereco
    {
        public long IdEndereco { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
    public class Telefone
    {
        public long IdTelefone { get; set; }
        public string NrTelefone { get; set; }
        public int TipoTelefone { get; set; }
    }


    public class Conta
    {
        public Conta()
        {
            DiasVencimento = new HashSet<int>();
        }

        public string NrConta { get; set; }

        public string NrCartao { get; set; }

        public string Produto { get; set; }

        public DateTime DtAtraso { get; set; }

        public int DiasAtraso { get; set; }

        public decimal VlSaldoAtraso { get; set; }

        public decimal VlPagamentoMinimo { get; set; }

        public string ImagemCartao { get; set; }

        public string TipoConta { get; set; }

        public int ORG { get; set; }

        public int LOGO { get; set; }

        public ICollection<int> DiasVencimento { get; set; }

    }
}


