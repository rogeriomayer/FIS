using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.Fis.Models
{
    public class BilletRequest
    {
        public string Account { get; set; }
        public string CPF { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public string CEP { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string UF { get; set; }
        public decimal Value { get; set; }
        public string Date { get; set; }
    }
    public class BilletResponse
    {
        public long IdBillet { get; set; }
        public string Number { get; set; }
        public string CodeBar { get; set; }
        public bool Registered { get; set; }
        public byte[] PDF { get; set; }
    }
}
