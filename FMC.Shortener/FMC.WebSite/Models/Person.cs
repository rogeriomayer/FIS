using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.Shortener.Models
{
    public class PersonResponse
    {
        public PersonResponse()
        {
            Phones = new HashSet<Phone>();
            Emails = new HashSet<Emails>();
            Accounts = new HashSet<Account>();
        }

        public long IdPerson { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public Address Address { get; set; }
        public ICollection<Phone> Phones { get; set; }
        public ICollection<Emails> Emails { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }

    public class Emails
    {
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }

    public class Address
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
    public class Phone
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
        public Agreement Agreement { get; set; }

    }

    public class PersonRequest
    {
        public string Cpf { get; set; }
    }
}




