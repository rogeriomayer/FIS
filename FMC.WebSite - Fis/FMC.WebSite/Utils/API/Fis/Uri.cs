using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace FMC.Fis.Utils.API.Fis
{
    public class Uri
    {
        //public static string URL { get { return "https://10.40.0.30/fis/api/"; } }
        //public static string URL { get { return "https://200.243.238.232/fis/api/"; } }
        public static string URL { get { return "http://localhost:5831/api/"; } }



        #region GET
        public static string CardsP1(string cpf)
        {
            return string.Concat(URL, "p1/cards/", cpf);
        }

        public static string Customer(string customer)
        {
            return string.Concat(URL, "Customer/", customer);
        }
        public static string Agreement(string card)
        {
            return string.Concat(URL, "Customer/Agreement/", card);
        }

        public static string Remessa(DateTime dtInitial, DateTime dtFinal)
        {
            return string.Concat(URL, "Remessa/", dtInitial.ToString("yyyy-MM-dd"), "/", dtFinal.ToString("yyyy-MM-dd"));
        }

        public static string Remessa(string idRemessa)
        {
            return string.Concat(URL, "Remessa/", idRemessa);
        }

        public static string SetDownloadRemessa(string idRemessa, string idUserLogin)
        {
            return string.Concat(URL, "Remessa/SetDownloadRemessa", "/", idRemessa, "/", idUserLogin);
        }

        public static string Retorno(int dias)
        {
            return string.Concat(URL, "Remessa/Retorno/", dias.ToString());
        }

        #endregion

        #region POST
        public static string BilletP1()
        {
            return string.Concat(URL, "Billet/BilletP1");
        }
        public static string BilletP2()
        {
            return string.Concat(URL, "Billet/BilletP2");
        }
        public static string ChangePassword()
        {
            return string.Concat(URL, "Login/ChangePassword");
        }
        public static string CardP1()
        {
            return string.Concat(URL, "p1/card");
        }
        #endregion

        #region SET
        public static string Login()
        {
            return string.Concat(URL, "Login");
        }

        public static string Retorno()
        {
            return string.Concat(URL, "Remessa/Retorno");
        }
        #endregion
    }
}
