using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Utils.API.Serasa
{
    public static class Uri
    {
        public static string URL()
        {
            //return "http://189.112.212.179/serasa/bradescard.svc/";
            return "https://ws.apifmcbrasil.com.br/serasa/bradescard.svc/";
        
        }

        #region GET
        public static string GetUR80(string codProduto)
        {
            return string.Concat(URL(), "UR80/", codProduto, "/000");
        }

        public static string GetUR83(string codProduto)
        {
            return string.Concat(URL(), "UR83/", codProduto);
        }

        public static string GetUR84(string cpf)
        {
            return string.Concat(URL(), "UR84/", cpf, "/2");
        }
        public static string GetUR86(string cpf, string tipo = "0")
        {
            return string.Concat(URL(), "UR86/", cpf, "/", tipo);
        }

        public static string GetDscProduct(string org, string logo)
        {
            return string.Concat(URL(), "GetDscProduct/", org, "/", logo);
        }

        public static string GetPerson(string cpf)
        {
            return string.Concat(URL(), "GetPerson/", cpf);
        }

        public static string GetSerasaAgreementToday()
        {
            return string.Concat(URL(), "GetSerasaAgreementToday");
        }
        public static string GetSerasaAgreementTodayByCard(string nrCard)
        {
            return string.Concat(URL(), "GetSerasaAgreementTodayByCard/", nrCard);
        }
        public static string GetSerasaProduct(string id)
        {
            return string.Concat(URL(), "GetSerasaProduct/", id);
        }

        public static string GetDiscount(string diasAtraso, string parcela)
        {
            return string.Concat(URL(), "GetDiscount/", diasAtraso, "/", parcela);
        }

        public static string GetParcelamento(string numeroConta, string vlEntrada, string discount, string dtEntrada, string codIdent)
        {
            return string.Concat(URL(), "GetParcelamento/", numeroConta, "/", vlEntrada, "/", discount, "/", dtEntrada, "/", codIdent);
        }
        public static string GetPDF(string dtEntrace, string valor, string nrAccount, string cpf, string tipo = "P")
        {
            return string.Concat(URL(), "GetPDF/", dtEntrace, "/", valor, "/", nrAccount, "/", cpf, "/", tipo);
        }

        public static string SendBilletSerasa(string dtVencimento, string saldoDevedor, string numeroConta, string cpf, string email)
        {
            return string.Concat(URL(), "SendBilletSerasa/", dtVencimento, "/", saldoDevedor, "/", numeroConta, "/", cpf, "/", email);
        }

        public static string GetSerasaNavigationByPeriod(DateTime dateInitial, DateTime dateEnd)
        {
            return string.Concat(URL(), "GetSerasaNavigationByPeriod/", dateInitial.ToString("yyyy-MM-dd"),"/",dateEnd.ToString("yyyy-MM-dd"));
        }

        public static string GetSerasaSimulateByPeriod(DateTime dateInitial, DateTime dateEnd)
        {
            return string.Concat(URL(), "GetSerasaSimulateByPeriod/", dateInitial.ToString("yyyy-MM-dd"), "/", dateEnd.ToString("yyyy-MM-dd"));
        }
        
        public static string GetSerasaAgreementByPeriod(DateTime dateInitial, DateTime dateEnd)
        {
            return string.Concat(URL(), "GetSerasaAgreementByPeriod/", dateInitial.ToString("yyyy-MM-dd"), "/", dateEnd.ToString("yyyy-MM-dd"));
        }

        #endregion

        #region SET
        public static string SetDataPerson()
        {
            return string.Concat(URL(), "SetDataPerson");
        }

        public static string SetSerasaNavigation()
        {
            return string.Concat(URL(), "SetSerasaNavigation");
        }

        public static string SetSerasaProduct()
        {
            return string.Concat(URL(), "SetSerasaProduct");
        }

        public static string SetSerasaAgreement()
        {
            return string.Concat(URL(), "SetSerasaAgreement");
        }
        public static string SetSerasaAddress()
        {
            return string.Concat(URL(), "SetSerasaAddress");
        }

        public static string SetSerasaSimulate()
        {
            return string.Concat(URL(), "SetSerasaSimulate");
        }
        public static string SetSerasaBillet()
        {
            return string.Concat(URL(), "SetSerasaBillet");
        }
        public static string SetSerasaBilletIBI()
        {
            return string.Concat(URL(), "SetSerasaBilletIBI");
        }


        public static string UpdateSerasaProduct()
        {
            return string.Concat(URL(), "UpdateSerasaProduct");
        }


        #endregion

    }
}
