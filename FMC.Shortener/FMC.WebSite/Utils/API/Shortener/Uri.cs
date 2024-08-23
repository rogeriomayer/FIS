using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace FMC.Shortener.Utils.API.Shortener
{
    public class Uri
    {
        public static string URL { get { return "https://200.243.238.232/fis/api/"; } }
        //public static string URL { get { return "https://10.40.0.30/fis/api/"; } }

        #region GET
        public static string Customer(string customer)
        {
            return string.Concat(URL, "Customer/", customer);
        }
        public static string Agreement(string card)
        {
            return string.Concat(URL, "Customer/Agreement/", card);
        }

        public static string GetByCode(string code, string ip)
        {
            return string.Concat(URL, "shorturl/", code + "/" + ip);
        }
        #endregion

        #region POST

        public static string ShortURL()
        {
            return string.Concat(URL, "ShortURL");
        }
        #endregion

        #region SET
        public static string Login()
        {
            return string.Concat(URL, "Login");
        }


        #endregion
    }
}
