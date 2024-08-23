using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Utils
{
    public class Util
    {

        public static bool IsEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNumeric(string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

        public static bool IsDate(string s)
        {
            DateTime output;
            return DateTime.TryParse(s, out output);
        }

        public static DateTime GetDateString(string message)
        {
            try
            {
                Match match = Regex.Match(message, @"\d{2}\/\d{2}\/\d{4}");
                string date = match.Value;
                if (!string.IsNullOrEmpty(date))
                {
                    var dateTime = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                    return dateTime;
                }
                else
                    return DateTime.Today.AddDays(4);
            }
            catch
            {
                return DateTime.Today.AddDays(4);
            }
        }

    }
}
