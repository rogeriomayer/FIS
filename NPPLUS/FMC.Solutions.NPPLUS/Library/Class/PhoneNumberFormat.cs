using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.Class
{
    public class PhoneNumberFormat
    {
        public static string PhoneNumber(string number)
        {

            number = number.Replace(" ", "");
            if (number.Length >= 8)
            {
                try
                {
                    string dddTel = Convert.ToInt32(Regex.Replace(number.Substring(0, 5), "[^0-9]", "")).ToString();
                    string homeTel = Convert.ToInt64(Regex.Replace(number.Substring(5, number.Length - 5), "[^0-9]", "")).ToString();
                    homeTel = homeTel.Length > 8 ? Convert.ToInt64(Regex.Replace(number.Substring(5, number.Length - 5), "[^0-9]", "")).ToString() : homeTel;
                    if (homeTel.Length >= 8)
                    {
                        if (dddTel.Length <= 2)
                        {
                            dddTel = "(" + dddTel.PadLeft(2, '0') + ") ";
                        }
                        else
                        {
                            dddTel = dddTel.PadLeft(5, '0');
                            dddTel = "(" + dddTel.Substring(2, 2) + ") ";
                        }
                        if (homeTel.Substring(0, 1) == "9" && homeTel.Length > 8)
                        {
                            homeTel = homeTel.Substring(0, 5) + "-" + homeTel.Substring(5, 4);
                        }
                        else
                        {
                            homeTel = homeTel.Substring(0, 4) + "-" + homeTel.Substring(4, 4);
                        }
                    }
                    return dddTel + homeTel != "00" ? dddTel + homeTel : "";
                }
                catch
                {

                }
            }
            return "";
        }
    }
}
