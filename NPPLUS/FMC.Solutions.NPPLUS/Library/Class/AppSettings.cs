
using System;
using System.Linq;
using System.Diagnostics;

namespace FMC.Solutions.NPPLUS.Library.Class
{
    public class AppSettings
    {
        public static string Url_FIS_API
        {
            get
            {
                return new System.Configuration.AppSettingsReader().GetValue("Url_FIS_API", typeof(string)).ToString();
            }

        }

        public static byte PRODUCT_TYPE
        {
            get { return Convert.ToByte(new System.Configuration.AppSettingsReader().GetValue("PRODUCT_TYPE", typeof(string)).ToString()); }
        }
    }
}
