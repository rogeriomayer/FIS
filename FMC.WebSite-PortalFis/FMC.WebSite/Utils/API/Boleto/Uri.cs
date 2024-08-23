using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Utils.API.Boleto
{
    public static class Uri
    {
        public static string URL()
        {
            //return "http://189.112.212.179/FMC.Boleto/BoletoService.svc/";
            return "https://ws.apifmcbrasil.com.br/FMC.Boleto/BoletoService.svc/";
        
        }

        #region GET
        
        public static string GetPDF(string carteira, string origem, string idAgreement, string parcel)
        {
            return string.Concat(URL(), "GetPDF/", carteira, "/", origem, "/", idAgreement, "/", parcel);
        }
        #endregion

        #region SET



        #endregion

    }
}
