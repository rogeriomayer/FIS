using System;
using System.Collections.Generic;
using System.Linq;
using FMC.Digicob.Business.Model;

namespace FMC.FIS.Business.Code.Api.Cobmais
{
    public class DigicobAPI
    {
        private static string URL = "10.40.0.52/digicob";
        public static Customer GetCustomer(string document)
        {
            var headers = new Dictionary<string, string>();

            return RestApi.Get<Customer>(URL, "customer/" + document, null, headers);
        }

        public static ICollection<Contract> GetContract(string cpf, string dtNascimento)
        {
            var headers = new Dictionary<string, string>();

            return RestApi.Get<ICollection<Contract>>(URL, "contracts/" + cpf + "/" + dtNascimento, null, headers);
        }
        public static ICollection<CustomerContact> GetCustomerContacts(long customerId)
        {
            var headers = new Dictionary<string, string>();

            return RestApi.Get<ICollection<CustomerContact>>(URL, "customer/contacts/" + customerId, null, headers);
        }

    }
}
